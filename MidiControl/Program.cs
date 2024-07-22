using MidiControl;
using NAudio.Midi;

var arturiaControllerId = -1;
var notesPressed = 0;
HashSet<int> pressedNotesNumbersSet = new();

for (int device = 0; device < MidiIn.NumberOfDevices; device++)
{
    if (MidiIn.DeviceInfo(device).ProductName == "Arturia KeyStep 32")
    {
        arturiaControllerId = device;
    }
}

Console.WriteLine($"Selected device with Id: [{arturiaControllerId}]");

var deviceMidiIn = new MidiIn(arturiaControllerId);
deviceMidiIn.MessageReceived += DeviceMidiIn_MessageReceived;
deviceMidiIn.Start();

Console.ReadLine();

void DeviceMidiIn_MessageReceived(object? sender, MidiInMessageEventArgs? e)
{
    switch (e.MidiEvent.CommandCode)
    {
        case MidiCommandCode.NoteOn:
        {
            var casted = (NoteOnEvent)e.MidiEvent;
            if (pressedNotesNumbersSet.Contains(casted.NoteNumber))
            {
                notesPressed++;
                break;
            }
            
            notesPressed++;
            pressedNotesNumbersSet.Add(casted.NoteNumber);
            break;
        }

        case MidiCommandCode.NoteOff:
        {
            notesPressed--;
            if (notesPressed > 0)
            {
                break;
            }

            Console.WriteLine($"All notes released, total notes recorded: {pressedNotesNumbersSet.Count}");
            foreach (var note in pressedNotesNumbersSet)
            {
                Console.WriteLine($"note recorded: {note}");
            }
            
            CommandExecuter.TryLaunchCommand(pressedNotesNumbersSet);
            
            pressedNotesNumbersSet.Clear();
            Console.WriteLine("-------------------Buffer cleared-------------------");
            break;
        }
    }
}
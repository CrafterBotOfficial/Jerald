# Jerald
A lightweight robust mod for Gorilla Tag that allows other mods to appened new pages to the default Gorilla Tag computer.

## For Developers
For your page to be recognized the assembly must have the AutoRegisterAttribute and it must be in the same assembly as the plugin itself.
```cs
[assembly: AutoRegister]
namespace MyMod;
```
A simple page would look like the following.
```cs
[AutoRegister] // Tells Jerald to register this class
public class NotePadPage : Page
{
    public override string PageName => "Notes"; // This will be displayed in the function select screen

    private string note = Configuration.PersistantNote.Value;
    private string pageContents;

    public NotePadPage()
    {
        base.OnKeyPressed += (key) =>
        {
            switch (key.Binding)
            {
                case GorillaKeyboardBindings.delete:
                    note = note.Remove(note.Length - 1, 1);
                    break;
                case GorillaKeyboardBindings.option2 | GorillaKeyboardBindings.option3 | GorillaKeyboardBindings.down | GorillaKeyboardBindings.up:
                    // do nothing
                    break;
                case GorillaKeyboardBindings.enter:
                    Configuration.PersistantNote.Value = note;
                    Main.Config.Save();
                    break;
                case GorillaKeyboardBindings.option1:
                    note += " ";
                    break;
                default:
                    note += key.characterString;
                    break;
            }
            pageContents = "Write anything you want to remember below.\n" + note;
        };
    }

    public override string GetContent()
    {
        return pageContents;
    }
}
```

### Keys
| Key | Enum Binding | Is Function |
|---|---|---|
| 1 | one=1 | False |
| 2 | two=2 | False |
| 3 | three=3 | False |
| 4 | four=4 | False |
| 5 | five=5 | False |
| 6 | six=6 | False |
| 7 | seven=7 | False |
| 8 | eight=8 | False |
| 9 | nine=9 | False |
| 0 | zero=0 | False |
| Q | Q=33 | False |
| W | W=39 | False |
| E | E=21 | False |
| R | R=34 | False |
| T | T=36 | False |
| Y | Y=41 | False |
| U | U=37 | False |
| I | I=25 | False |
| O | O=31 | False |
| P | P=32 | False |
| A | A=17 | False |
| S | S=35 | False |
| D | D=20 | False |
| F | F=22 | False |
| G | G=23 | False |
| H | H=24 | False |
| J | J=26 | False |
| K | K=27 | False |
| L | L=28 | False |
| Z | Z=42 | False |
| X | X=40 | False |
| C | C=19 | False |
| V | V=38 | False |
| B | B=18 | False |
| N | N=30 | False |
| M | M=29 | False |
| delete | delete=12 | True |
| enter | enter=13 | True |
| option1 | option1=14 | True |
| option2 | option2=15 | True |
| option3 | option3=16 | True |
| up | up=10 | True |
| down | down=11 | True |

## Credits
<sub><sup><sub><sup>[Chin](https://github.com/Chin0303) for the idea</sup></sub></sup></sub>

This product is not affiliated with Another Axiom Inc. or its videogames Gorilla Tag and Orion Drift and is not endorsed or otherwise sponsored by Another Axiom. Portions of the materials contained herein are property of Another Axiom. ©2021 Another Axiom Inc.
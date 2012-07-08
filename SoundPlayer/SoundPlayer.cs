using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Fusao.AudioAssist.SoundPlayer
{
    /// <summary>
    /// Represents the type of sound to be played by the SoundPlayer class.
    /// </summary>
    public enum SoundType { Breakpoint, BreakpointHit, ExceptionHit, Step, GoToDefinition, FindAllReferences, Stop, Start };

    /// <summary>
    /// This class loads a number of hard-coded wave files into memory upon instantiation, and 
    /// causes them to play when the Play method is called. 
    /// </summary>
    public class SoundPlayer
    {
        /// <summary>
        /// Contains initialized versions of the SoundPlayer classes that
        /// have preloaded the audio files we're going to player. The dictionary
        /// values consist of Lists that contain all possible sounds for that 
        /// SoundType enum value, one of which is randomly chosen when the <see cref="PlayNoise"/>
        /// function is called.
        /// </summary>
        private Dictionary<SoundType, List<System.Media.SoundPlayer>> _sounds
            = new Dictionary<SoundType, List<System.Media.SoundPlayer>>();

        /// <summary>
        /// Random number generator used to determine which sound out of all possible sounds
        /// for a SoundType is to be played. 
        /// </summary>
        Random _randomSoundIndexPicker = new Random();

        /// <summary>
        /// Initializes a new instance of SoundPlayer, loading all of the hard-coded sounds
        /// into memory. Throws FileNotFound exceptions upon not finding the sound files, which
        /// are assumed to be in a subdirectory called "debugNoises" in the same folder as this
        /// assembly.
        /// </summary>
        public SoundPlayer()
        {
            // We assume that the sound files are stored in a directory called "debugNoises"
            // in the same folder as the containing assembly. 
            string basePath = System.IO.Path.GetDirectoryName(typeof(SoundPlayer).Assembly.Location);
            string soundFilesPath = System.IO.Path.Combine(basePath, "debugNoises") + @"\";

            // Build up the private dictionary-list containing SoundPlayers loaded with
            // all of our sound files. 
            BuildDictionaryEntry(SoundType.Breakpoint, new string[] { soundFilesPath + "breakpoint.wav" });
            BuildDictionaryEntry(SoundType.BreakpointHit, new string[] { soundFilesPath + "breakpointhit.wav" });
            BuildDictionaryEntry(SoundType.ExceptionHit, new string[] { soundFilesPath + "exception.wav" });
            BuildDictionaryEntry(SoundType.Step, new string[] { soundFilesPath + "step.wav", soundFilesPath + "kachunk.wav" });
            BuildDictionaryEntry(SoundType.Stop, new string[] { soundFilesPath + "stop.wav" });
            BuildDictionaryEntry(SoundType.GoToDefinition, new string[] { soundFilesPath + "gotodefinition.wav" });
            BuildDictionaryEntry(SoundType.FindAllReferences, new string[] { soundFilesPath + "findallref.wav" });
            BuildDictionaryEntry(SoundType.Start, new string[] { soundFilesPath + "start.wav" });
        }

        private void BuildDictionaryEntry(SoundType targetSoundType, string[] soundFiles)
        {
            _sounds[targetSoundType] = BuildSoundPlayerList(soundFiles);
        }

        private List<System.Media.SoundPlayer> BuildSoundPlayerList(string[] waveFiles)
        {
            List<System.Media.SoundPlayer> soundPlayerList = new List<System.Media.SoundPlayer>();
            foreach (string waveFile in waveFiles)
            {
                System.Media.SoundPlayer s =
                    new System.Media.SoundPlayer(waveFile);
                // Loads the wave file into memory.
                s.Load();
                soundPlayerList.Add(s);
            }
            return soundPlayerList;
        }

        /// <summary>
        /// Asynchronously plays a randomly chosen sound from the list of 
        /// hard-coded sounds that are part of the specified SoundType.
        /// </summary>
        /// <param name="which">The SoundType to select a sound from.</param>
        public void PlayNoise(SoundType which)
        {
            int whichOneToPlay = _randomSoundIndexPicker.Next(_sounds[which].Count);
            // Play is an asynchronous method. 
            _sounds[which][whichOneToPlay].Play();
        }
    }
}

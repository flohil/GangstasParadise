using Grapevine;
using Grapevine.Server;
using NAudio.Lame;
using NAudio.Wave;
using Newtonsoft.Json.Linq;
using System;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Speech.AudioFormat;
using System.Speech.Synthesis;
using System.Text;

namespace GangstasParadiseServer {
    public sealed class MyResource : RESTResource {

        private const string READ_GENERATED_TEXT_FROM = @"D:\development\git\FHTW\mal2\backend\finalSamples\Scheme\top25_3.txt";
        private const string WORKING_DIRECTORY_FOR_SHELL = @"D:\development\git\FHTW\mal2\backend";
        private const string MUSIC_DIRECTORY = @"C:\Users\pKAisinger\Music\";

        private const string TTS_MP3 = MUSIC_DIRECTORY + "TextSample.mp3";
        private const string BEAT_MP3 = MUSIC_DIRECTORY + "BeatSample.mp3";

        private const string TTS_WAV = MUSIC_DIRECTORY + "TextSample.wav";
        private const string BEAT_WAV = MUSIC_DIRECTORY + "BeatSample.wav";

        private const string MIXED_MP3 = MUSIC_DIRECTORY + "MixedSample.mp3";

        private const int LENGTH_OF_READ_TEXT = 60;

        private static bool _useFreshlyGeneratedText = true;

        [RESTRoute(Method = HttpMethod.GET, PathInfo = @"^/generate")]
        public void StartSongGeneration(HttpListenerContext context) {

            string generatedText = "";
            if (_useFreshlyGeneratedText) {
                //JObject initParameter = GetJsonPayload(context.Request);
                //CallShell(initParameter.GetValue("primetext").ToString(), int.Parse(initParameter.GetValue("numberoflines").ToString()));
                generatedText = CallShellAnGenerateRap("", 10);
            } else {
                // get text from generated text file
                using (StreamReader reader = new StreamReader(READ_GENERATED_TEXT_FROM)) {
                    generatedText = reader.ReadToEnd();
                }
            }
           

            this.SendTextResponse(context, generatedText.Substring(0, LENGTH_OF_READ_TEXT), Encoding.UTF8);            
        }

        [RESTRoute(Method = HttpMethod.GET, PathInfo = @"^/mix")]
        public void GetGeneratedFile(HttpListenerContext context) {
            MixTextWithBeat();
            this.SendFileResponse(context, MIXED_MP3);
        }

        private string CallShellAnGenerateRap(string primeText, int numberOfLines) {
            string cmdText = @"python naiveSample.py --forward_dir=save\top25_3 --reversed_dir=reversed\top25_3 --post_dir=post\top25_3 --sample=2 -n " + numberOfLines;
            //string cmdText = @"python naiveSample.py --forward_dir=save\top25_3 --reversed_dir=reversed\top25_3 --post_dir=post\top25_3 --sample=2 -n 100 > output.txt";
            //string cmdText = @"python schemeSample.py --forward_dir=save/top25_3 --reversed_dir=reversed/top25_3 --post_dir=post/top25_3 --sample=2 -n 100 --prime=\""start text\"" > output.txt";
            Process p = new Process();
            p.StartInfo.WorkingDirectory = WORKING_DIRECTORY_FOR_SHELL;
            p.StartInfo.UseShellExecute = false;
            p.StartInfo.FileName = @"C:\Windows\System32\cmd.exe";
            p.StartInfo.Verb = "runas";
            p.StartInfo.Arguments = "/c " + cmdText;
            p.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
            p.StartInfo.RedirectStandardOutput = true;
            p.Start();
            string output = p.StandardOutput.ReadToEnd();
            p.WaitForExit();
            return output;
        }        

        private void GenerateTextToSpeechFile(string generatedText) {
            // Initialize a new instance of the SpeechSynthesizer
            using (SpeechSynthesizer synth = new SpeechSynthesizer()) {

                //// Output information about all of the installed voices. 
                //Console.WriteLine("Installed voices -");
                //foreach (InstalledVoice voice in synth.GetInstalledVoices()) {
                //    Console.WriteLine(" Voice Name: " + voice.VoiceInfo.Description);
                //}
                ////Voice Name: Microsoft Hedda Desktop
                ////Voice Name: Microsoft Zira Desktop
                ////Voice Name: Microsoft Hazel Desktop

                synth.SelectVoice("Microsoft Hedda Desktop");
                synth.Volume = 100;
                synth.Rate = 0;

                // Configure the audio output. 
                MemoryStream ms = new MemoryStream();
                synth.SetOutputToWaveStream(ms);

                synth.Speak(generatedText.Substring(0, LENGTH_OF_READ_TEXT));

                // generate mp3
                ConvertWavStreamToMp3File(ref ms, TTS_MP3);
            }
        }

        private void MixTextWithBeat() {

            // resample music to mix it afterwards            
            Resample(BEAT_MP3, BEAT_WAV);
            Resample(TTS_MP3, TTS_WAV);
            
            WaveFileReader mpbackground = new WaveFileReader(BEAT_WAV);
            WaveFileReader mpMessage = new WaveFileReader(TTS_WAV);

            //convert them into wave stream or decode the mp3 file
            WaveStream background = WaveFormatConversionStream.CreatePcmStream(mpbackground);
            WaveStream message = WaveFormatConversionStream.CreatePcmStream(mpMessage);

            var mixer = new WaveMixerStream32();
            mixer.AutoStop = true;

            //var messageOffset = background.TotalTime;
            //var messageOffsetted = new WaveOffsetStream(
            //    message, TimeSpan.FromSeconds(10), TimeSpan.Zero, message.TotalTime.Subtract(TimeSpan.FromSeconds(30)));

            var background32 = new WaveChannel32(background);
            background32.PadWithZeroes = false;

            // set the volume of background file
            background32.Volume = 0.5f;
            // add stream into the mixer
            mixer.AddInputStream(background32);

            //var message32 = new WaveChannel32(messageOffsetted);
            var message32 = new WaveChannel32(message);
            message32.PadWithZeroes = false;

            // set the volume of 2nd mp3 song
            message32.Volume = 1.0f;
            mixer.AddInputStream(message32);

            var wave32 = new Wave32To16Stream(mixer);

            //encode the wave stream into mp3
            ConvertWavStreamToMp3File(wave32, MIXED_MP3);
        }

        private void Resample(string inFile, string outFile) {
            using (var reader = new Mp3FileReader(inFile)) {
                var outFormat = new WaveFormat(16000, 16, 1);
                using (var resampler = new MediaFoundationResampler(reader, outFormat)) {
                    // resampler.ResamplerQuality = 60;
                    WaveFileWriter.CreateWaveFile(outFile, resampler);
                }
            }
        }

        private void ConvertWavStreamToMp3File(Wave32To16Stream wavFile, string saveToPath) {
            using (var retMs = new MemoryStream())
            using (var wtr = new LameMP3FileWriter(saveToPath, wavFile.WaveFormat, LAMEPreset.ABR_128)) {
                wavFile.CopyTo(wtr);
            }
        }

        private void ConvertWavStreamToMp3File(ref MemoryStream ms, string saveToPath) {
            //rewind to beginning of stream
            ms.Seek(0, SeekOrigin.Begin);

            using (var retMs = new MemoryStream())
            using (var rdr = new WaveFileReader(ms))
            using (var wtr = new LameMP3FileWriter(saveToPath, rdr.WaveFormat, LAMEPreset.ABR_128)) {
                rdr.CopyTo(wtr);
            }
        }
    }
}

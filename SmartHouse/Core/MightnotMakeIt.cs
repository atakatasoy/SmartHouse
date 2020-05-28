using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Speech.Recognition;

namespace SmartHouse
{
    public class MightnotMakeIt
    {
        private SpeechRecognitionEngine Recogengine { get; set; }

        private Choices Options { get; set; }

        private GrammarBuilder Builder { get; set; }

        private Grammar BuiltGrammar { get; set; }

        private List<string> ListOfOoptions { get; set; } = new List<string>();

        public MightnotMakeIt()
        {

            Recogengine = new SpeechRecognitionEngine();

            Recogengine.SetInputToDefaultAudioDevice();

            Options = new Choices();

            ListOfOoptions.FetchOptionsFromDb();

            Options.Add(ListOfOoptions.ToArray());

            Builder = new GrammarBuilder(Options);

            BuiltGrammar = new Grammar(Builder);

            Recogengine.LoadGrammar(BuiltGrammar);

            Recogengine.BabbleTimeout = new TimeSpan(0, 0, 2);

            Recogengine.EndSilenceTimeout = new TimeSpan(0, 0, 2);

            Recogengine.SpeechRecognized += Recogengine_SpeechRecognized;

            Recogengine.RecognizeCompleted += Recogengine_RecognizeCompleted;

            Recogengine.SpeechRecognitionRejected += Recogengine_SpeechRecognitionRejected;

            Recogengine.SpeechDetected += Recogengine_SpeechDetected;

        }

        private void Recogengine_SpeechDetected(object sender, SpeechDetectedEventArgs e)
        {
            Console.WriteLine("Listening Master");
        }

        private void Recogengine_SpeechRecognitionRejected(object sender, SpeechRecognitionRejectedEventArgs e)
        {
            Console.WriteLine("I can not understand you");
        }

        private void Recogengine_RecognizeCompleted(object sender, RecognizeCompletedEventArgs e)
        {
           
        }

        private void Recogengine_SpeechRecognized(object sender, SpeechRecognizedEventArgs e)
        {
            CommandStructs.CreateCommandStructs(e.Result.Words);
        }

        public void Start()
        {
            Recogengine.RecognizeAsync(RecognizeMode.Multiple);
        }
        
        private void Stop()
        {
            Recogengine.RecognizeAsyncStop();
        }

       
    }
}

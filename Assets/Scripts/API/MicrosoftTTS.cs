using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading.Tasks;
using Microsoft.CognitiveServices.Speech;
using Microsoft.CognitiveServices.Speech.Audio;

public class MicrosoftTTS : MonoBehaviour    
{
    static string YourSubscriptionKey = "074073d5e4954fad81b11c7a8ce0e6b7";
    static string YourServiceRegion = "francecentral";

    [SerializeField]
    string textToSay;


    static void OutputSpeechSynthesisResult(SpeechSynthesisResult speechSynthesisResult, string text)
    {
        switch (speechSynthesisResult.Reason)
        {
            case ResultReason.SynthesizingAudioCompleted:
               Debug.Log($"Speech synthesized for text: [{text}]");
                break;
            case ResultReason.Canceled:
                var cancellation = SpeechSynthesisCancellationDetails.FromResult(speechSynthesisResult);
                Debug.Log($"CANCELED: Reason={cancellation.Reason}");

                if (cancellation.Reason == CancellationReason.Error)
                {
                    Debug.Log($"CANCELED: ErrorCode={cancellation.ErrorCode}");
                    Debug.Log($"CANCELED: ErrorDetails=[{cancellation.ErrorDetails}]");
                    Debug.Log($"CANCELED: Did you set the speech resource key and region values?");
                }
                break;
            default:
                break;
        }
    }

    public async static Task Speech(string text)
    {
        var speechConfig = SpeechConfig.FromSubscription(YourSubscriptionKey, YourServiceRegion);
        // The language of the voice that speaks.
        speechConfig.SpeechSynthesisVoiceName = "fr-FR-DeniseNeural";

        using (var speechSynthesizer = new SpeechSynthesizer(speechConfig))
        {

            var speechSynthesisResult = await speechSynthesizer.SpeakTextAsync(text);
            OutputSpeechSynthesisResult(speechSynthesisResult, text);
        }
    }

    // Start is called before the first frame update
    async void Start()
    {
        await Speech(textToSay);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

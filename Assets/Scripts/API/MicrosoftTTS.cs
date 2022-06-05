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

    //Voices to choose from
    [SerializeField]
    Voices voice;
    public enum Voices
    {
        Denise,
        Henri,
        Ariane,
        Fabrice,
        Sylvie,
        Antoine,
        Jean
    }

    private static string voiceName;

    //Api result treatment
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

    //Launch API request
    public async static Task Speech(string text)
    {
        var speechConfig = SpeechConfig.FromSubscription(YourSubscriptionKey, YourServiceRegion);
        
        // The language of the voice that speaks.
        speechConfig.SpeechSynthesisVoiceName = voiceName;

        using (var speechSynthesizer = new SpeechSynthesizer(speechConfig))
        {

            var speechSynthesisResult = await speechSynthesizer.SpeakTextAsync(text);
            OutputSpeechSynthesisResult(speechSynthesisResult, text);
        }
    }

    
    private void Update()
    {
        //Set the right voice parametre for the voice
        switch (voice)
        {
            case Voices.Denise:
                voiceName = "fr-FR-DeniseNeural";
                break;
            case Voices.Henri:
                voiceName = "fr-FR-HenriNeural";
                break;
            case Voices.Ariane:
                voiceName = "fr-CH-ArianeNeural	";
                break;
            case Voices.Fabrice:
                voiceName = "fr-CH-FabriceNeural";
                break;
            case Voices.Sylvie:
                voiceName = "fr-CA-SylvieNeural";
                break;
            case Voices.Antoine:
                voiceName = "fr-CA-AntoineNeural";
                break;
            case Voices.Jean:
                voiceName = "fr-CA-JeanNeural";
                break;
        }

    }

}

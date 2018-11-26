using System.Collections.Generic;
using System.IO;
using System.Linq;
using Microsoft.ProjectOxford.Emotion;
using System.Threading.Tasks;

namespace App5.Class
{
   public class ServiceEmotions
    {
        static string key = "03f8ac73a1974d17a00db9aba785c883";
        
        public static async Task<Dictionary<string, float>> GetEmotions(Stream stream)
        {
            EmotionServiceClient cliente = new EmotionServiceClient(key);
            var emociones = await cliente.RecognizeAsync(stream);

            if (emociones == null || emociones.Count() == 0)
                return null;

            return emociones[0].Scores.ToRankedList().ToDictionary(x => x.Key, x => x.Value);
            
        }

    }
}
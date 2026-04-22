using UnityEngine;
using UnityEngine.Networking;
using System.Collections;

public class GestorTrivia : MonoBehaviour
{
    // Variable pública solicitada para almacenar los datos (se verá en el Inspector)
    public RespuestaTrivia datosTrivia;
    private string urlApi = "https://opentdb.com/api.php?amount=10";

    void Start()
    {
        // Iniciamos la corrutina para llamar a la API
        StartCoroutine(ObtenerDatosTrivia());
    }

    IEnumerator ObtenerDatosTrivia()
    {
        using (UnityWebRequest solicitudWeb = UnityWebRequest.Get(urlApi))
        {
            // Esperamos a que la petición termine
            yield return solicitudWeb.SendWebRequest();

            if (solicitudWeb.result == UnityWebRequest.Result.Success)
            {
                // Serializar el JSON a nuestras clases de C#
                string jsonRecibido = solicitudWeb.downloadHandler.text;
                datosTrivia = JsonUtility.FromJson<RespuestaTrivia>(jsonRecibido);

                // Comprobar que tenemos datos e imprimir lo solicitado
                if (datosTrivia != null && datosTrivia.resultados.Count > 0)
                {
                    Debug.Log("Número de preguntas solicitadas: " + datosTrivia.resultados.Count);

                    // Obtenemos 4 datos de la primera pregunta (índice 0)
                    Pregunta primeraP = datosTrivia.resultados[0];
                    Debug.Log("--- Datos de la Primera Pregunta ---");
                    Debug.Log("1. Categoría: " + primeraP.categoria);
                    Debug.Log("2. Dificultad: " + primeraP.dificultad);
                    Debug.Log("3. Texto de la pregunta: " + primeraP.pregunta);
                    Debug.Log("4. Respuesta correcta: " + primeraP.respuesta_correcta);
                }
            }
            else
            {
                Debug.LogError("Error al conectar con la API: " + solicitudWeb.error);
            }
        }
    }
}
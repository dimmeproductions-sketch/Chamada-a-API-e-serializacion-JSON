using UnityEngine;
using System;
using System.Collections.Generic;

[Serializable]
public class Pregunta
{
    public string categoria;
    public string tipo;
    public string dificultad;
    public string pregunta;
    public string respuesta_correcta;
    public List<string> respuestas_incorrecta;
}

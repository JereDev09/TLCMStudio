public enum SpawnType
{
    None,
    PorTiempo,
    PorDistancia,
}

//lugares a los que vas pasando a medida que avanzas de edificio
public enum Entornos
{
    None,
    Ciudad,
    Calles,
    Todos
}

public enum EstadoCañon
{
    NONE,
    APUNTANDO,
    PREPARANDO_DISPARO,
    DISPARANDO
}

public enum TipoRebote
{
    None, // no toca la velocidad Y
    Fijo, // pone una velocidad Y concreta
    Conservativo // devuelve un porcentaje de la velocidad con la que llegaste
}
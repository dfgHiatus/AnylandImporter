namespace AnylandImporter.Common;

/// <summary>
/// Represents the placements and settings of an area.
/// </summary>
public class Placements
{
    public bool ok { get; set; }
    public Area area { get; set; }
    public string areaId { get; set; }
    public string areaName { get; set; }
    public string areaKey { get; set; }
    public string areaCreatorId { get; set; }
    public bool isPrivate { get; set; }
    public bool isZeroGravity { get; set; }
    public bool hasFloatingDust { get; set; }
    public bool isCopyable { get; set; }
    public bool onlyOwnerSetsLocks { get; set; }
    public bool isExcluded { get; set; }
    public string _environmentType { get; set; }
    public string environmentChangersJSON { get; set; }
    public string settingsJSON { get; set; } // Unused?
    public bool requestorIsEditor { get; set; }
    public bool requestorIsListEditor { get; set; }
    public bool requestorIsOwner { get; set; }

    /// <summary>
    /// Gets or sets an array of all the area's Thing placements.
    /// </summary>
    public Placement[] placements { get; set; }

    /// <summary>
    /// Gets or sets the settings for the area.
    /// </summary>
    public Settings settings { get; set; }
}

/// <summary>
/// Represents a placement of a Thing.
/// </summary>
public class Placement
{
    /// <summary>
    /// Gets or sets the Thing id.
    /// </summary>
    public string Id { get; set; }
    public string Tid { get; set; }

    /// <summary>
    /// Gets or sets the position in x-y-z coordinates.
    /// </summary>
    public Position P { get; set; }

    /// <summary>
    /// Gets or sets the rotation in x-y-z coordinates.
    /// </summary>
    public Rotation R { get; set; }

    /// <summary>
    /// Gets or sets the scale in x-y-z coordinates.
    /// </summary>
    public float S { get; set; }

    /// <summary>
    /// Gets or sets the Thing attributes.
    /// </summary>
    public ThingAttribute[] A { get; set; }
}

/// <summary>
/// Settings that define the attributes of a global area.
/// </summary>
public class EnvironmentChanger
{
    public string Name { get; set; }
    public Rotation Rotation { get; set; }
    public float Scale { get; set; }
    public Color Color { get; set; }
}

/// <summary>
/// A position in x-y-z coordinates.
/// </summary>
public class Position
{
    public float x { get; set; }
    public float y { get; set; }
    public float z { get; set; }
}

/// <summary>
/// A rotation in x-y-z coordinates.
/// </summary>
public class Rotation
{
    public float x { get; set; }
    public float y { get; set; }
    public float z { get; set; }
}

/// <summary>
/// A color in RGBA format.
/// </summary>
public class Color
{
    public float r { get; set; }
    public float g { get; set; }
    public float b { get; set; }
    public int a { get; set; }
}

/// <summary>
/// Represents the settings for an area.
/// </summary>
public class Settings
{
    /// <summary>
    /// Gets or sets the filters, if any, that are set outside the default values.
    /// </summary>
    public Filters filters { get; set; }

    /// <summary>
    /// Gets or sets the rotation x-y-z of the environment sun light.
    /// </summary>
    public float[] sunDirection { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether the sun is set to omit shadows.
    /// </summary>
    public bool sunOmitsShadow { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether there's floating dust in the area.
    /// </summary>
    public bool hasFloatingDust { get; set; }
}

/// <summary>
/// Represents the filters for an area.
/// </summary>
public class Filters
{
    /// <summary>
    /// Gets or sets the saturation, if any.
    /// </summary>
    public string sa { get; set; }

    /// <summary>
    /// Gets or sets the bloom.
    /// </summary>
    public string bl { get; set; }

    /// <summary>
    /// Gets or sets the contrast.
    /// </summary>
    public string co { get; set; }

    /// <summary>
    /// Gets or sets the brightness.
    /// </summary>
    public string br { get; set; }

    /// <summary>
    /// Gets or sets the color reduction.
    /// </summary>
    public string cr { get; set; }

    /// <summary>
    /// Gets or sets the hue shift.
    /// </summary>
    public string hs { get; set; }

    /// <summary>
    /// Gets or sets the distance tint.
    /// </summary>
    public string dt { get; set; }

    /// <summary>
    /// Gets or sets the inversion.
    /// </summary>
    public string iv { get; set; }

    /// <summary>
    /// Gets or sets the heat map.
    /// </summary>
    public string hm { get; set; }
}


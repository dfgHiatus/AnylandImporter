namespace AnylandImporter.Models;

/// <summary>
/// Represents the placements and settings of an area.
/// </summary>
public class Placements
{
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

/// <summary>
/// Represents a placement of a Thing.
/// </summary>
public class Placement
{
    /// <summary>
    /// Gets or sets the Thing id.
    /// </summary>
    public string i { get; set; }

    /// <summary>
    /// Gets or sets the position in x-y-z coordinates.
    /// </summary>
    public float[] p { get; set; }

    /// <summary>
    /// Gets or sets the rotation in x-y-z coordinates.
    /// </summary>
    public float[] r { get; set; }

    /// <summary>
    /// Gets or sets the scale in x-y-z coordinates.
    /// </summary>
    public float[] s { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether the placement is locked.
    /// </summary>
    public bool locked { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether the placement is invisible to editors.
    /// </summary>
    public bool invisibleToEditors { get; set; }

    /// <summary>
    /// Gets or sets the distance at which the placement should be shown.
    /// </summary>
    public float distanceToShow { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether scripts and states are suppressed for this placement.
    /// </summary>
    public bool suppressScriptsAndStates { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether collisions are suppressed for this placement.
    /// </summary>
    public bool suppressCollisions { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether lights are suppressed for this placement.
    /// </summary>
    public bool suppressLights { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether particles are suppressed for this placement.
    /// </summary>
    public bool suppressParticles { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether holdable settings are suppressed for this placement.
    /// </summary>
    public bool suppressHoldable { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether Show at Distance is suppressed for this placement.
    /// </summary>
    public bool suppressShowAtDistance { get; set; }
}

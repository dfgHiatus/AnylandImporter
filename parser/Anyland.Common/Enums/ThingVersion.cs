/// <summary>
/// Emulates an old bug-used-as-feature behavior for downwards-compatibility reasons.
/// "Send nearby" commands of things saved at this version are treated to mean "send one nearby"
/// when part of emitted or thrown items stuck to someone.
/// </summary>
public enum ThingVersion
{
    /// <summary>
    /// If the thingPart includes an image, the material will be forced to become default and white
    /// (and black during loading). In version 3+ it will be left as is, and e.g. glowing becomes
    /// a glowing image, and thingPart colors are being respected.
    /// </summary>
    Version1 = 1,

    /// <summary>
    /// The default font material in version 4+ is non-glowing. Version 3- fonts will take on the
    /// glow material.
    /// </summary>
    Version2 = 2,

    /// <summary>
    /// In version 4-, bouncy & slidy for thrown/emitted things were both selectable but mutually
    /// exclusive in effect (defaulting on bouncy). Since v5+ they mix.
    /// </summary>
    Version3 = 3,

    /// <summary>
    /// In version 5-, "tell web" and "tell any web" didn't exist as special tell scope commands,
    /// so they will be understood as being tell/tell any with "web" as data.
    /// </summary>
    Version4 = 4,

    /// <summary>
    /// In version 6-, one unit of the "set constant rotation" command equals 10 rotation degrees
    /// (instead of 1 in later versions).
    /// </summary>
    Version5 = 5,

    /// <summary>
    /// In version 8, the "tell in front" and "tell first in front" commands were added. In version 7-,
    /// "in front"/ "first in front" are considered normal tell data text.
    /// </summary>
    Version6 = 6,

    /// <summary>
    /// As of version 9, sounds played via the Loop command adhere to the Thing's Surround Sound attribute.
    /// In version 8-, that setting was ignored.
    /// </summary>
    Version7 = 7,

    /// <summary>
    /// The current thing version.
    /// </summary>
    Version8 = 8
}
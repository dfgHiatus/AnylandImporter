using System.Collections.Generic;
using System.Text;

namespace AnylandImporter;

public class ThingDescriptor
{
    /// <summary>
    /// Gets or sets the thing name.
    /// </summary>
    public string n { get; set; } // thing name

    /// <summary>
    /// Gets or sets the thing attributes.
    /// </summary>
    public List<ThingAttribute> a { get; set; } // thing attributes

    /// <summary>
    /// Gets or sets an optional description of the wooden table (up to 200 characters).
    /// </summary>
    public string d { get; set; } // an optional description of up to 200 characters

    /// <summary>
    /// Gets or sets included name-ids for emits and more.
    /// </summary>
    public List<List<string>> inc { get; set; } // included name-ids for emits and more

    /// <summary>
    /// Gets or sets the thing version.
    /// </summary>
    public ThingVersion v { get; set; } // thing version

    /// <summary>
    /// Gets or sets the mass of the wooden table.
    /// </summary>
    public double tp_m { get; set; } // thing physics mass

    /// <summary>
    /// Gets or sets the drag of the wooden table.
    /// </summary>
    public double tp_d { get; set; } // thing physics drag

    /// <summary>
    /// Gets or sets the angular drag of the wooden table.
    /// </summary>
    public double tp_ad { get; set; } // thing physics angular drag

    /// <summary>
    /// Gets or sets a value indicating whether the wooden table's position is locked.
    /// </summary>
    public bool tp_lp { get; set; } // thing physics lock position

    /// <summary>
    /// Gets or sets a value indicating whether the wooden table's rotation is locked.
    /// </summary>
    public bool tp_lr { get; set; } // thing physics lock rotation

    /// <summary>
    /// Gets or sets the parts of the wooden table.
    /// </summary>
    public List<Part> p { get; set; } // parts

    /// <summary>
    /// states
    /// </summary>
    public List<State> s { get; set; } // states

    public override string ToString()
    {
        StringBuilder sb = new StringBuilder();

        sb.AppendLine($"Thing Name: {n ?? "No name"}");
        if (a != null)
            sb.AppendLine($"Thing Attributes: [{string.Join(", ", a)}]");
        sb.AppendLine($"Description: {d ?? "No desc."}");
        sb.AppendLine("Included Name-Ids:");
        if (inc != null)
        {
            foreach (var nameIdPair in inc)
            {
                sb.AppendLine($"    Name: {nameIdPair[0]}, Id: {nameIdPair[1]}");
            }
        }
        sb.AppendLine($"Thing Version: [{string.Join(", ", v)}]");
        sb.AppendLine("Thing Physics Properties:");
        sb.AppendLine($"    Mass: {tp_m}");
        sb.AppendLine($"    Drag: {tp_d}");
        sb.AppendLine($"    Angular Drag: {tp_ad}");
        sb.AppendLine($"    Lock Position: {tp_lp}");
        sb.AppendLine($"    Lock Rotation: {tp_lr}");
        sb.AppendLine("Parts:");

        foreach (var part in p)
        {
            sb.AppendLine($"  Part:");
            sb.AppendLine($"    Base Shape Type: {part.b}");
            sb.AppendLine($"    Material Type: {part.t}");
            if (part.a != null)
                sb.AppendLine($"    Part Attributes: [{string.Join(", ", part.a)}]");
            sb.AppendLine($"    Part Name: {part.n ?? "No name"}");
            sb.AppendLine($"    Part Identifier: {part.id ?? "No ID"}");
            sb.AppendLine($"    Text: {part.e ?? "No text"}");
            sb.AppendLine($"    Line Height: {part.lh}");
            sb.AppendLine($"    Image URL: {part.im ?? "No Image URL"}");
            sb.AppendLine($"    Image Type: {part.imt ?? "No Image Type"}");
            sb.AppendLine($"    Particle System Type: {part.pr}");
            sb.AppendLine($"    Texture Type Layer 1: {part.t1}");
            sb.AppendLine($"    Texture Type Layer 2: {part.t2}");
            if (part.ac != null)
            {
                sb.AppendLine("    Auto-Continuation/Auto-Complete:");
                sb.AppendLine($"      Id: {part.ac.id}");
                sb.AppendLine($"      Count: {part.ac.c}");
                sb.AppendLine($"      Waves Amount: {part.ac.w}");
                sb.AppendLine($"      Position Randomization Factor: {part.ac.rp}");
                sb.AppendLine($"      Rotation Randomization Factor: {part.ac.rr}");
                sb.AppendLine($"      Scale Randomization Factor: {part.ac.rs}");
            }
            // sb.AppendLine("    Changed Vertices:");
            // sb.AppendLine("    Auto-Attached Body Parts:");
        }

        if (s != null)
        {
            sb.AppendLine("    States:");
            var firstState = s.FirstOrDefault();
            if (firstState != null)
            {
                sb.AppendLine($"      Position: [{string.Join(", ", firstState.p)}]");
                sb.AppendLine($"      Rotation: [{string.Join(", ", firstState.r)}]");
                sb.AppendLine($"      Scale: [{string.Join(", ", firstState.s)}]");
                sb.AppendLine($"      Color: [{string.Join(", ", firstState.c)}]");
            }
        }

        return sb.ToString();
    }
}

/// <summary>
/// Represents a part of the wooden table.
/// </summary>
public class Part
{
    // Define properties for the table part, including base shape type, material type, attributes, name, unique identifier,
    // text, line height, included sub-things, placed sub-things, image URL, image type, particle system type, texture types,
    // auto-continuation/auto-complete, changed vertices, states, and auto-attached body parts of head
    // ...

    /// <summary>
    /// Gets or sets the base shape type of the table part.
    /// </summary>
    public ThingPartBase b { get; set; } // base shape type

    /// <summary>
    /// Gets or sets the material type of the table part.
    /// </summary>
    public MaterialType t { get; set; } // material type

    /// <summary>
    /// Gets or sets the attributes of the table part.
    /// </summary>
    public List<ThingAttribute> a { get; set; } // thing part attributes

    /// <summary>
    /// Gets or sets an optional name for the table part (up to 100 characters).
    /// </summary>
    public string n { get; set; } // an optional part name of up to 100 characters

    /// <summary>
    /// Gets or sets the unique identifier for the table part.
    /// </summary>
    public string id { get; set; } // optional unique part identifier

    /// <summary>
    /// Gets or sets the text associated with the table part.
    /// </summary>
    public string e { get; set; } // text

    /// <summary>
    /// Gets or sets the line height of the text.
    /// </summary>
    public double lh { get; set; } // text line height

    /// <summary>
    /// Gets or sets the included sub-things in the table part.
    /// </summary>
    public List<IncludedSubThing> i { get; set; } // included sub-things

    /// <summary>
    /// Gets or sets the placed sub-things in the table part.
    /// </summary>
    public List<PlacedSubThing> su { get; set; } // placed sub-things

    /// <summary>
    /// Gets or sets the image URL associated with the table part.
    /// </summary>
    public string im { get; set; } // image URL

    /// <summary>
    /// Gets or sets the image type (if PNG instead of default JPG).
    /// </summary>
    public string imt { get; set; } // image type

    /// <summary>
    /// particle system type
    /// </summary>
    public ParticleSystemType pr;

    /// <summary>
    /// texture type 1
    /// </summary>
    public TextureType t1;

    /// <summary>
    /// texture type 2
    /// </summary>
    public TextureType t2;

    /// <summary>
    /// auto continuation
    /// </summary>
    public AutoContinuation ac;

    // public List<ChangedVertices> c;
    // public List<AutoAttachedBodyPartOfHead> bod;

}

/// <summary>
/// Represents an included sub-thing in a table part.
/// </summary>
public class IncludedSubThing
{
    public string t; // thing id of sub-thing
    public double[] p; // relative position
    public double[] r; // relative rotation
    public string n; // optional name override
    public ThingAttribute[] a; // optional attributes to invert, e.g. uncollidable
}

/// <summary>
/// Represents a placed sub-thing in a table part, referring to area placements.
/// </summary>
public class PlacedSubThing
{
    public string i; // placement id
    public string t; // thing id of sub-thing
    public double[] p; // relative position
    public double[] r; // relative rotation
}

/// <summary>
/// Represents the auto-continuation/auto-complete properties of a table part.
/// </summary>
public class AutoContinuation
{
    public string id; // thing part identifier reference
    public int c; // count
    public int w; // waves amount, if > 1
    public double rp; // position randomization factor
    public double rr; // rotation randomization factor
    public double rs; // scale randomization factor
}

public class State
{
    public double[] p; // position
    public double[] r; // rotation
    public double[] s; // scale
    public double[] c; // color
}

///// <summary>
///// Represents the changed vertices of a table part.
///// </summary>
//public class ChangedVertices
//{
//    // Define properties for changed vertices
//    // ...

//    // ... Define properties for changed vertices
//}

///// <summary>
///// Represents the states of a table part.
///// </summary>
//public class PartState
//{
//    // Define properties for part states, including position, rotation, scale, color, and behavior script lines
//    // ...

//    // ... Define properties for part states
//}

///// <summary>
///// Represents auto-attached body parts of a table part.
///// </summary>
//public class AutoAttachedBodyParts
//{
//    // Define properties for auto-attached body parts, including head, upper torso, lower torso, leg left, leg right,
//    // head top, arm left, and arm right
//    // ...

//    // ... Define properties for auto-attached body parts
//}

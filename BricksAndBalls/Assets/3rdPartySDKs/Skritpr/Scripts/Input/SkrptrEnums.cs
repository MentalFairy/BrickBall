namespace Skrptr
{ 
    /// <summary>
    /// Used for SkrptrMoveOutsideOfScreen - points direction of exit.
    /// </summary>
    public enum SlideDirection { Left, Right, Up, Down };

    /// <summary>
    /// Used for SkrptrRotation.
    /// Absolute - Absolute values of rotation, locks at -360 to 360
    /// Relative - adds / substracts from current rotation - no locks.
    /// </summary>
    public enum RotateType { Absolute, Relative };

    /// <summary>
    /// Enum containing all possible events to fire on an element.
    /// </summary>
    [System.Flags]
    public enum SkrptrEvent {

        None = 0,
        Click = 1,
        Select = 2,
        Deselect = 4,
        Enable = 8,
        Disable = 16,
        Hide = 32,
        Show = 64,
        Lock = 128,
        Unlock = 256,
        HoverEnter = 512,
        HoverExit = 1024,
        Check = 2048,
        Uncheck = 4096,
        LongPress = 8192,       
        Loop = 16384,
    };

    /// <summary>
    /// Used for directional input - points 4 coordinal direction + in / out.
    /// </summary>
    [System.Flags]
    public enum NeighbourDirection
    {
        None = 0,
        Up = 1,
        Down= 2,
        Left= 4,
        Right = 8,
        Back = 16,
        Click = 32,
    };

    /// <summary>
    /// Supported input types to interract with skrptrElements.
    /// </summary>
    public enum SkrptrInputType
    {
        None,
        Mouse,
        Keyboard,
        Touch
    }
}

using System;

namespace SynchronizingResourceAccess;

public static class SharedObjects
{
    public static string? Message; // a shared resource.
    public static object Conch = new(); // a shared object to lock.
    public static int Counter; // another shared resource.
}

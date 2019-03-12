# Does `GetFunctionPointerForDelegate` honor `UnmanagedFunctionPointerAttribute`?

This repository is a quick experiment to check whether or not `GetFunctionPointerForDelegate` honors `UnmanagedFunctionPointerAttribute`.

At the time of writing, `GetFunctionPointerForDelegate` states that it produces a function pointer to a `stdcall` function, but it actually honors `UnmanagedFunctionPointerAttribute` when it is present for both .NET Core and .NET Framework.

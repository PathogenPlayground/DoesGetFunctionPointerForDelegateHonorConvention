#define ARES_API extern "C" __declspec(dllexport)

typedef int (__cdecl *CdeclCallback)(int, int);
typedef int (__stdcall *StdcallCallback)(int, int);

ARES_API int __cdecl TakesCdecl(CdeclCallback callback)
{
    return callback(7, 42);
}

ARES_API int __stdcall TakesStdcall(StdcallCallback callback)
{
    return callback(7, 42);
}

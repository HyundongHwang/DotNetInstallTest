// The following ifdef block is the standard way of creating macros which make exporting 
// from a DLL simpler. All files within this DLL are compiled with the MYDLL_EXPORTS
// symbol defined on the command line. This symbol should not be defined on any project
// that uses this DLL. This way any other project whose source files include this file see 
// MYDLL_API functions as being imported from a DLL, whereas this DLL sees symbols
// defined with this macro as being exported.
#ifdef MYDLL_EXPORTS
#define MYDLL_API __declspec(dllexport)
#else
#define MYDLL_API __declspec(dllimport)
#endif

// This class is exported from the MyDll.dll
class MYDLL_API CMyDll {
public:
    CMyDll(void);
    // TODO: add your methods here.
};



#ifdef __cplusplus
extern "C" {
#endif

extern MYDLL_API int nMyDll;

MYDLL_API int fnMyDll(void);

MYDLL_API int GetStringLength(LPCTSTR sz);



MYDLL_API char* GetSampleString();

MYDLL_API wchar_t* GetSampleWString();



typedef void(*OnCallbackDelegateW) (wchar_t*);

typedef void(*OnCallbackDelegate) (char*);

MYDLL_API void SayHello(char* wName, OnCallbackDelegate callback);

MYDLL_API void SayHelloW(wchar_t* wName, OnCallbackDelegateW callback);



#ifdef __cplusplus
}
#endif
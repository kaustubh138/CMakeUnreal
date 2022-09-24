#ifndef _CMAKEUNREAL_MODULE_HPP_
#define _CMAKEUNREAL_MODULE_HPP_

#include "Engine.h"
#include "Modules/ModuleInterface.h"
#include "Modules/ModuleManager.h"

class FCMakeUnrealEditorModule
    : public IModuleInterface
{
public:
    virtual void StartupModule() override;
    virtual void ShutdownModule() override;
};
#endif //! _CMAKEUNREAL_MODULE_HPP_
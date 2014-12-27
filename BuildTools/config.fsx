// include Fake lib
#r @"../packages/FAKE/tools/FakeLib.dll"

open Fake

// Properties
[<AutoOpen>]
module Config =
    let buildDir     = "./build/"
    let testDir      = "./"
    let srcDir       = "./"
    let alarmsSln    = "BowlingKata.sln"

    let environments = ["Debug"; "Release"]
    let buildMode () = getBuildParamOrDefault "buildMode" "Release"
    let version      = "1.0.0.0"
    let targetWithEnv target env = sprintf "%s:%s" target env

    let setBuildMode = setEnvironVar "buildMode"

    let debugMode   () = setBuildMode "Debug"
    let releaseMode () = setBuildMode "Release"
    let stagingMode () = setBuildMode "Staging"

    let setParams defaults =
        { defaults with
            Targets = ["Build"]
            Properties =
                [
                    "Optimize", "True"
                    "Platform", "Any CPU"
                    "Configuration", buildMode()
                ]
        }



        




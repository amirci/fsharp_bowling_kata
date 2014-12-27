// include Fake lib
#r @"../packages/FAKE/tools/FakeLib.dll"

open System.IO
open Fake

RestorePackages()

#load "./config.fsx"
#load "./Test.fsx"

open Config

// Targets
Target "Clean" (fun _ ->
    CleanDirs [buildDir]
)

let addBuildTarget name env sln =
    let rebuild config = {(setParams config) with Targets = ["Rebuild"]}
    Target (targetWithEnv name env) (fun _ ->
        setBuildMode env
        build rebuild sln
    )

environments |> Seq.iter (fun env -> 
    addBuildTarget "Bowling" env alarmsSln

    Target  (targetWithEnv "All" env) (fun _ ->
        run (targetWithEnv "Bowling" env)
    )
)




// start build
RunTargetOrDefault "All:Debug"
#r @"../packages/FAKE/tools/FakeLib.dll"

#load "./config.fsx"

open System.IO
open Fake
open System

open Config

let allTests = Map["BowlingKata", "BowlingKata"]

let testFiles testPrj = sprintf "%s/%s/bin/%s/*.dll" testDir testPrj (buildMode())

let runTests files =
    files
      |> NUnit (fun p ->
          {p with
             DisableShadowCopy = true
             OutputFile = testDir + "/TestResults.xml" })


let addTestTarget targetName testPrj =
    let csProj = sprintf "%s/%s/%s.csproj" testDir testPrj testPrj
    let fsProj = sprintf "%s/%s/%s.fsproj" testDir testPrj testPrj
    let prjFile = (if fileExists csProj then csProj else fsProj)

    Target ("Test:" + targetName) (fun _ ->
        (debugMode ())
        let testParams defaults = 
            {(setParams defaults) with
                Properties = 
                [
                    "Configuration", buildMode()
                    "Platform", "AnyCPU"
                ]
            }

        build testParams prjFile
        !! (testFiles testPrj) |> runTests
    )


Target "Test" (fun _ -> allTests |> Map.iter (fun name _ -> run ("Test:" + name)))

allTests |> Map.iter (fun name prj -> addTestTarget name prj)



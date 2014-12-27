namespace BowlingKata

module Tests =

    open System
    open NUnit.Framework
    open FsCheck
    open FsCheck.NUnit
    open FsUnit
    open System.Diagnostics

    open BowlingGame

    type Int32 with
        member this.framesOf = List.init this

    let sum (a, b) = a + b
    let sumFrames = Seq.sumBy sum

    let strike _ = (10, 0)
    let gutter _ = ( 0, 0)
    let spare  _ = ( 9, 1)

    type LameGames =
        static member Game() =
            Gen.choose (1, 9)
            |> Gen.map (fun i -> (i-1, 1))
            |> Gen.listOfLength 10
            |> Arb.fromGen
            

    type SpareGames =
        static member Game() =
            Gen.choose (1, 9)
            |> Gen.map (fun i -> (i, 10-i))
            |> Gen.listOfLength 10
            |> Arb.fromGen

    [<Property(Arbitrary=[|typeof<LameGames>|])>]
    let ``Any lame game is just the sum of frames`` (game:Game) =
        game |> Score = (game |> sumFrames)
        
    [<Test>]
    let ``A gutter game is zero`` () =
        let lame = 10 .framesOf gutter
        lame |> Score |> should equal 0

    [<Test>]
    let ``A perfect game is 300`` () =
        let perfect = 12 .framesOf strike
        perfect |> Score |> should equal 300


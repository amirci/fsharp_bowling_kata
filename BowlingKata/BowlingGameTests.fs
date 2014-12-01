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
        member this.framesOf = Seq.init this

    let validFrame (a, b) = a >= 0 && b >= 0

    let sum (a, b) = a + b
    let sumFrames = Seq.sumBy sum
    let noSpareStrike f = f |> sum < 10  
    
    //let isValid = Seq.forall validFrame

    let printIt (game:Game) =
        Debug.WriteLine(game)
        true

    let noSparesOrStrikes (game:Game) = 
        true // game |> Seq.forall noSpareStrike

    let strike _ = (10, 0)
    let gutter _ = ( 0, 0)

    let (.=.) left right = left = right |@ sprintf "%A = %A" left right

    type ValidGames =
        static member Game() =
            Gen.choose (1, 9)
            |> Gen.map (fun i -> (i-1, 1))
            |> Gen.listOfLength 10
            |> Arb.fromGen
            

    [<Property(Arbitrary=[|typeof<ValidGames>|])>]
    let ``Any game without spares or strikes is just the sum`` (game:Game) =
        game |> Score .=. (game |> sumFrames)
        

    [<Test>]
    let ``A gutter game is zero`` () =
        let lame = 10 .framesOf gutter
        lame |> Score |> should equal 0

    [<Test>]
    let ``A perfect game is 300`` () =
        let perfect = 12 .framesOf strike
        perfect |> Score |> should equal 300
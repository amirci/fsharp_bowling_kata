namespace BowlingKata

[<AutoOpen>]
module BowlingGame =

    type Frame = int * int
    type Game  = Frame list

    let Score aGame = aGame |> Seq.sumBy (fun (a, b) -> a + b)
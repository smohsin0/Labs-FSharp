module PolygonArea

open System
open System.IO

[<Struct>]
type Point = { X: int; Y: int }

let crossProduct p1 p2 = (p1.Y * p2.X - p1.X * p2.Y) |> double

let area (ps: Point list) =
    let first = ps |> List.take 1
    let pairs = Seq.append ps first |> Seq.pairwise
    let sum = Seq.sumBy ((<||) crossProduct) pairs
    Math.Abs sum / 2.0

let (|Split|) (c:char) (txt:string) = txt.Split (c) |> Array.toList

let trim (txt:string) = txt.Trim()

let main (stdin:TextReader) =
    let count = stdin.ReadLine() |> int 
    let parse = function
        | Split ' ' [p1; p2] -> Some ({ X=p1 |> int; Y= p2 |> int })
        | _ -> None
        
    let points = 
        Seq.init count (ignore >> stdin.ReadLine >> trim)
        |> Seq.choose parse
        |> Seq.toList
        
    printfn "%.1f" (area points)
namespace FuncationFSharp

module Say =
    let hello name =
        printfn "Hello %s" name

    let add num1 num2 =
        num1 + num2

    let add10 num = add 10
    let add20 num = add 20


    add 1 3

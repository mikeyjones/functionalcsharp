namespace FuncationFSharp

module Say =
    let hello name =
        printfn "Hello %s" name

    let add num1 num2 =
        num1 + num2

    let add10 num = add 10
    let add20 num = add 20

    type Account = {
        name: string
        balance: double
    }

    type Logger = {
        writeLine: string
    }

    let deposit account amount =
        { account with balance = account.balance + amount }

    
    let depositWithLogging logger account amount =
        logger.writeLine $"Adding {amount} to account {account.name}"

    let deposit' = depositWithLogging ConsoleLogger     

    add 1 3
    add10 20

    10 |> add10 |> add20
    add20(add10(10))

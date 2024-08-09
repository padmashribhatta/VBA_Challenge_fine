Sub stock_analysis()
    Dim ws As Worksheet
    Dim ticker As String
    Dim openPrice As Double
    Dim closePrice As Double
    Dim quarterlyChange As Double
    Dim percentChange As Double
    Dim totalVolume As Double
    Dim lastRow As Long
    Dim summaryRow As Integer
    Dim greatestIncrease As Double
    Dim greatestDecrease As Double
    Dim greatestVolume As Double
    Dim greatestIncreaseTicker As String
    Dim greatestDecreaseTicker As String
    Dim greatestVolumeTicker As String

    For Each ws In Worksheets
        ws.Activate
        lastRow = ws.Cells(Rows.Count, 1).End(xlUp).Row
        summaryRow = 2

        ' Set headers for summary table
        ws.Cells(1, 9).Value = "Ticker"
        ws.Cells(1, 10).Value = "Quarterly Change"
        ws.Cells(1, 11).Value = "Percent Change"
        ws.Cells(1, 12).Value = "Total Volume"

        greatestIncrease = 0
        greatestDecrease = 0
        greatestVolume = 0

        For i = 2 To lastRow
            If ws.Cells(i + 1, 1).Value <> ws.Cells(i, 1).Value Then
                ticker = ws.Cells(i, 1).Value
                openPrice = ws.Cells(i, 3).Value
                closePrice = ws.Cells(i, 6).Value
                quarterlyChange = closePrice - openPrice
                If openPrice <> 0 Then
                    percentChange = (quarterlyChange / openPrice) * 100
                Else
                    percentChange = 0
                End If
                totalVolume = Application.WorksheetFunction.Sum(ws.Range("G2:G" & i))

                ' Output to summary table
                ws.Cells(summaryRow, 9).Value = ticker
                ws.Cells(summaryRow, 10).Value = quarterlyChange
                ws.Cells(summaryRow, 11).Value = percentChange
                ws.Cells(summaryRow, 12).Value = totalVolume

                ' Apply color formatting
                If quarterlyChange > 0 Then
                    ws.Cells(summaryRow, 10).Interior.Color = RGB(0, 255, 0) ' Green for positive
                ElseIf quarterlyChange < 0 Then
                    ws.Cells(summaryRow, 10).Interior.Color = RGB(255, 0, 0) ' Red for negative
                Else
                    ws.Cells(summaryRow, 10).Interior.Color = RGB(255, 255, 255) ' White for zero
                End If

                ' Check for greatest increase, decrease, and volume
                If percentChange > greatestIncrease Then
                    greatestIncrease = percentChange
                    greatestIncreaseTicker = ticker
                End If
                If percentChange < greatestDecrease Then
                    greatestDecrease = percentChange
                    greatestDecreaseTicker = ticker
                End If
                If totalVolume > greatestVolume Then
                    greatestVolume = totalVolume
                    greatestVolumeTicker = ticker
                End If

                summaryRow = summaryRow + 1
            End If
        Next i

        ' Output greatest values
        ws.Cells(2, 15).Value = "Greatest % Increase"
        ws.Cells(2, 16).Value = greatestIncreaseTicker
        ws.Cells(2, 17).Value = greatestIncrease

        ws.Cells(3, 15).Value = "Greatest % Decrease"
        ws.Cells(3, 16).Value = greatestDecreaseTicker
        ws.Cells(3, 17).Value = greatestDecrease

        ws.Cells(4, 15).Value = "Greatest Total Volume"
        ws.Cells(4, 16).Value = greatestVolumeTicker
        ws.Cells(4, 17).Value = greatestVolume
    Next ws
End Sub

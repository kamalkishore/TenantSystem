class BillList extends React.Component {
    constructor(props) {
        super(props);        
        this.state = {bills: props.bills}
    }

    render() {
        const bills = this.props.bills;
        const billItems = bills.map((bill) => {
            return (
                <table key={bill.Id} className="tenant-bill-template">
                    <ThreeColSpanRow rowText1={bill.TenantName} />
                    <ThreeColSpanRow classValue="border-tr" rowText1="Electricity Bill for Month for" rowText2={bill.CurrentMonthReadingDate} />
                    <BillRow textValue="Current Month Reading" dateValue={bill.CurrentMonthReadingDate} numberValue={bill.CurrentMonthReading} />
                    <BillRow textValue="Previous Month Reading" dateValue={bill.PreviousMonthReadingDate} numberValue={bill.PreviousMonthReading} />
                    <BillRow textValue="Total Unit Consumed" dateValue="" numberValue={bill.UnitConsumed} />
                    <AmountPayableRow unitPriceValue={bill.PerUnitPrice} amountValue={bill.CurrentMonthPayableAmount} />
                    <BillRow textValue="Previous Month Pending Amount" dateValue="" numberValue={bill.PreviousMonthPendingAmount} />
                    <BillRow textValue="Last Amount Paid Date" dateValue={bill.LastPaidAmountDate} numberValue={bill.LastPaidAmount} />
                    <BillRow classValue="border-tr" textValue="Total Amount Payable" dateValue="" numberValue={bill.TotalPayableAmount} />
                </table>
            )
        }
    );

    return (
        <div> {billItems}</div>
    );
}
}

function ThreeColSpanRow(props) {
    if (props.rowText2) {
        var dateValue = moment(props.rowText2).format("MMMM-YYYY");
    }
    return (
        <tr className={props.classValue}>
            <td colSpan="3">{props.rowText1} {dateValue}</td>
        </tr>
    );
}

function BillRow(props) {
    if (props.dateValue) {
        var dateValue = moment(props.dateValue).format("DD-MMMM-YYYY");
    }
    return (
        <tr className={props.classValue}>
            <td> {props.textValue} {dateValue}
            </td>
            <td className="arrow"></td>
            <td>{props.numberValue}
            </td>
        </tr>
    );
}

function AmountPayableRow(props) {
    return (
        <tr className="border-tr">
            <td>Amount Payable for the Month (Rs {props.unitPriceValue} Per Unit)
                       </td>
            <td className="arrow"></td>
            <td>{props.amountValue}
            </td>
        </tr>
    );
}
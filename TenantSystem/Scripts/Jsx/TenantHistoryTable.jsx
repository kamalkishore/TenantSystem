class TenantHistoryTable extends React.Component {
    constructor(props) {
        super(props);
        this.state = { history: props.tenantHistory, tenantName: props.TenantName }
    }

    render() {
        const historyData = this.props.tenantHistory;
        const tenantName = this.props.tenantName;
        const historyRows = historyData.map((history) => {
            return (                
                <HistoryRow classValue="border-tr"
                    CurrentMonthMeterReadingDate={history.CurrentMonthMeterReadingDate}
                    CurrentMonthMeterReading={history.CurrentMonthMeterReading}
                    PreviousMonthMeterReadingDate={history.PreviousMonthMeterReadingDate}
                    PreviousMonthMeterReading={history.PreviousMonthMeterReading}
                    PerUnitPrice={history.PerUnitPrice}
                    AmountPayable={history.AmountPayable}
                    PaymentDate={history.PaymentDate}
                    PaymentAmount={history.PaymentAmount}
                    TotalAmountPayable={history.TotalAmountPayable}
                />
            )
        }
    );

        return (
            <div>
                <h1>{ tenantName }</h1>
                <table className="tenant-history-template table-bordered table-striped1">
                    <tbody>
                    <tr>
                            <th>Current Month Date
                                </th>
                            <th>Current Month Reading
                                </th>
                            <th>Previous Month Date
                                </th>
                            <th>Previous Month Reading
                                </th>
                            <th>Unit Price
                                </th>
                            <th>Amount Payable
                                </th>
                            <th>Payment Date
                                </th>
                            <th>Payment Amount
                                </th>
                            <th>Amount Payable
                                </th>
                        </tr>
                {historyRows}
                </tbody>
                    </table>
                </div>
    );
}
}

function HistoryRow(props) {
    return (
        <tr className="border-tr1">
            <td>{moment(props.CurrentMonthMeterReadingDate).format("DD-MMM-YYYY")}
            </td>
            <td>{props.CurrentMonthMeterReading }
            </td>
            <td>{moment(props.PreviousMonthMeterReadingDate).format("DD-MMM-YYYY")}
            </td>
            <td>{props.PreviousMonthMeterReading }
            </td>
            <td>{props.PerUnitPrice }
            </td>
            <td>{props.AmountPayable }
            </td>
            <td>{moment(props.PaymentDate).format("DD-MMM-YYYY")}
            </td>
            <td>{props.PaymentAmount }
            </td>
            <td>{props.TotalAmountPayable }
            </td>
        </tr>
    );
}
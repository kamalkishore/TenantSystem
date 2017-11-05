class SearchControl extends React.Component {
    constructor(props) {
        super(props);
        //this.handleLoginClick = this.handleLoginClick.bind(this);
        //this.handleLogoutClick = this.handleLogoutClick.bind(this);
        this.state = { bills: [] };
        this.getBills = this.getBills.bind(this);
        this.setBills = this.setBills.bind(this);
        this.onTenantSelection = this.onTenantSelection.bind(this);
    }

    // componentDidMount() {
    //    this.getBills(2);
    //}

    setBills(data) {
        this.setState({
            bills: data.bills
        });
     }

    getBills(selectedTenantId) {

        let optionGetBillDetailsOfSelectedTenant = {
            url: "GetBillDetailsOfSelectedTenant",
            datatype: "json",
            data: { tenantId: selectedTenantId },
            type: "GET",
            success: this.setBills,
            error: function (xhr, event) {
                console.log("error");
            }
        };
        $.ajax(optionGetBillDetailsOfSelectedTenant);
    }

    onTenantSelection(tenantId) {
        this.getBills(tenantId)
    }

    render() {
        return (
            <div>
                <SelectControl onSelectedItemChange={tenantId => this.onTenantSelection(tenantId)} /><br />
                <DateControl /><br />
                <BillList bills={this.state.bills} />
            </div>
        );
    }
}

class SelectControl extends React.Component {
    constructor(props) {
        super(props);
        //this.handleLoginClick = this.handleLoginClick.bind(this);
        //this.handleLogoutClick = this.handleLogoutClick.bind(this);
        this.state = { tenants: [], selectedTenantId: 0 };
        this.getTenants = this.getTenants.bind(this);
        this.setTenants = this.setTenants.bind(this);
        this.onSelectItem = this.onSelectItem.bind(this);
    }

    componentDidMount() {
        this.getTenants();
    }

    setTenants(data) {
        this.setState({
            tenants: data.tenants
        });
    }

    getTenants() {
        let optionGetTenants = {
            url: "GetTenants",
            datatype: "json",
            type: "GET",
            success: this.setTenants,
            error: function (xhr, event) {
                console.log("error");
            }
        };
        $.ajax(optionGetTenants);
    }

    onSelectItem(tenantId) {
        console.log("select control selected id : " + tenantId);
        this.setState({
            selectedTenantId: tenantId
        });

        this.props.onSelectedItemChange(tenantId);
    }

    render() {
        const tenants = this.state.tenants;
        const options = tenants.map((tenant) => {
            return (<option key={tenant.Id} value={tenant.Id}>{tenant.FullName} </option>);
        });

        return (
            <div className="display-generated-bill">
                Select Tenant   :
                <select onChange={ev => this.onSelectItem(ev.target.value)} >
                    <option key="0" value="0">Select </option>)
                    {options}
                </select>
            </div>

        );
    }
}

    function DateControl() {
        return (
            <div className="display-generated-bill">
                Select Month   :
            <input type="month" id="txtMonth" required />
            </div>
        );
    }

    class BillList extends React.Component {
        constructor(props) {
            super(props);
            console.log("bill list component: ");
            console.log(this.props)
            this.state = {bills: props.bills}
        }

        componentDidMount() {
            console.log("componentDidMount: ");
            console.log(this.props);
        }

        render() {
            const bills = this.props.bills;
            const billItems = bills.map((bill) => {
                return (
                    <table key={bill.Id} className="tenant-bill-template">
                        <ThreeColSpanRow rowText1={bill.TenantName} />
                        <ThreeColSpanRow classValue="border-tr" rowText1="Electricity Bill for Month for" rowText2={bill.PreviousMonthReadingDate} />
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
    return (
        <tr className={props.classValue}>
            <td colSpan="3">{props.rowText1} {props.rowText2}</td>
        </tr>
    );
}

function BillRow(props) {
    return (
        <tr className={props.classValue}>
            <td> {props.textValue} {props.dateValue}
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

React.render(
    <SearchControl />,
    document.getElementById('generatedBillContainer')
);
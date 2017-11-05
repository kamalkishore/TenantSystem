class SearchControl extends React.Component {
    constructor(props) {
        super(props);
        this.state = { bills: [] };
        this.getBills = this.getBills.bind(this);
        this.setBills = this.setBills.bind(this);
        this.onTenantSelection = this.onTenantSelection.bind(this);
    }
    
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


    function DateControl() {
        return (
            <div className="display-generated-bill">
                Select Month   :
            <input type="month" id="txtMonth" required />
            </div>
        );
    }
React.render(
    <SearchControl />,
    document.getElementById('generatedBillContainer')
);
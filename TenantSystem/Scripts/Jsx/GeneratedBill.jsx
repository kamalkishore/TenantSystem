class SearchControl extends React.Component {
    constructor(props) {
        super(props);
        this.state = { bills: [], selectedTenantId : 0, selectedMonth: "" };
        this.getBills = this.getBills.bind(this);
        this.setBills = this.setBills.bind(this);
        this.onTenantSelection = this.onTenantSelection.bind(this);
    }
    
    setBills(data) {
        this.setState({
            bills: data.bills
        });
     }

    getBills() {        
        let tenantId = this.state.selectedTenantId;
        let selectedDateValue = this.state.selectedMonth;
        let selectedDate = selectedDateValue.split("-");

        let optionGetBillDetailsOfSelectedTenant = {
            url: "GetBillDetailsOfSelectedTenant",
            datatype: "json",
            data: { tenantId: tenantId },
            type: "GET",
            success: this.setBills,
            error: function (xhr, event) {
                console.log("error");
            }
        };

        let optionGetBillDetailsOfSelectedMonthAndTenant = {
            url: "GetBillDetailsOfSelectedMonthAndTenant",
            datatype: "json",
            data: { month: selectedDate[1], year: selectedDate[0], tenantId: tenantId },
            type: "GET",
            success: this.setBills,
            error: function (xhr, event) {
                console.log("error");
            }
        };

        let optionGetBillDetailsOfSelectedMonth = {
            url: "GetBillDetailsOfSelectedMonth",
            datatype: "json",
            data: { month: selectedDate[1], year: selectedDate[0] },
            type: "GET",
            success: this.setBills,
            error: function (xhr, event) {
                console.log("error");
            }
        };

        let tenantAndDateBothSelected = (selectedDateValue !== "" && tenantId !== "" && tenantId !== 0);

        if (tenantAndDateBothSelected) {
            console.log("tenantId !== 0 : " + (tenantId !== 0));
            console.log("tenantId : " + tenantId );
            $.ajax(optionGetBillDetailsOfSelectedMonthAndTenant);
        }
        else if (tenantId !== "" && tenantId !== 0) {
            $.ajax(optionGetBillDetailsOfSelectedTenant);
        }
        else {
            $.ajax(optionGetBillDetailsOfSelectedMonth);
        }

        //$.ajax(optionGetBillDetailsOfSelectedTenant);
    }

    onTenantSelection(tenantId) {
        this.setState({
            selectedTenantId: tenantId
        }, this.getBills
        );        
    }

    onMonthSelection(selectedMonth) {
        this.setState({
            selectedMonth: selectedMonth
        }, this.getBills
        );        
    }

    render() {
        return (
            <div>
                <DateControl onMonthSelect={month => this.onMonthSelection(month)} /><br />
                <SelectControl onSelectedItemChange={tenantId => this.onTenantSelection(tenantId)} /><br />                
                <BillList bills={this.state.bills} />
            </div>
        );
    }
}

React.render(
    <SearchControl />,
    document.getElementById('generatedBillContainer')
);
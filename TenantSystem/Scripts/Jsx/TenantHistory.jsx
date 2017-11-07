class TenantHistory extends React.Component {
    constructor(props) {
        super(props);
        this.state = { tenantHistory: [], selectedTenantId : 0, selectedTenantName : "" };
        this.getTenantHistory = this.getTenantHistory.bind(this);
        this.setTenantHistory = this.setTenantHistory.bind(this);
        this.onTenantSelection = this.onTenantSelection.bind(this);
    }
    
    setTenantHistory(data) {
        this.setState({
            tenantHistory: data.bills,
            selectedTenantName: data.TenantName
        });
        console.log(data);
     }

    getTenantHistory() {        
        let tenantId = this.state.selectedTenantId;

        var optionGetHistoryOfSelectedTenant = {
            url: "GetHistoryOfSelectedTenant",
            datatype: "json",
            data: { tenantId: tenantId },
            type: "GET",
            success: this.setTenantHistory,
            error: function (xhr, event) {
                console.log("error");
            }
        };

        $.ajax(optionGetHistoryOfSelectedTenant);
    }

    onTenantSelection(tenantId) {
        this.setState({
            selectedTenantId: tenantId
        }, this.getTenantHistory
        );        
    }    

    render() {
        return (
            <div>
                <SelectControl onSelectedItemChange={tenantId => this.onTenantSelection(tenantId)} /><br />
                <TenantHistoryTable tenantName={this.state.selectedTenantName} tenantHistory={this.state.tenantHistory} />
            </div>
        );
    }
}

React.render(
    <TenantHistory />,
    document.getElementById('ContainerTenantHistory')
);
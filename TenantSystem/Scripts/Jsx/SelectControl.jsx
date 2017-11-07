class SelectControl extends React.Component {
    constructor(props) {
        super(props);
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
                Select Tenant {'   :   '}
                <select onChange={ev => this.onSelectItem(ev.target.value)} >
                    <option key="0" value="0">Select </option>)
                    {options}
                </select>
            </div>

        );
    }
}
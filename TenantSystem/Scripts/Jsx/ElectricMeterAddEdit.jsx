class ElectricMeterControl extends React.Component {
    constructor(props) {
        super(props);
        this.state = { meterName : "", meterType: 0, dateOfInstallation: "", initialReading:0  };
        this.handleSubmit = this.handleSubmit.bind(this);
        this.onDateChange = this.onDateChange.bind(this);
        this.onNameChange = this.onNameChange.bind(this);
        this.onReadingChange = this.onReadingChange.bind(this);
        this.addMeter = this.addMeter.bind(this);
        this.successMethod = this.successMethod.bind(this);
    }

    successMethod() {
        console.log("Meter Added Successfully");
    }

    addMeter() {
        console.log("add called");
        let optionAddMeter = {
            url: "AddMeter",
            datatype: "json",
            data: {
                    MeterType: this.state.meterType,
                    Name: this.state.meterName,
                    DateOfMeterInstalled: this.state.dateOfInstallation,
                    InitialReading: this.state.initialReading
                   },
            type: "POST",
            success: this.successMethod,
            error: function (xhr, event) {
                console.log("error");
            }
        };
        
        $.ajax(optionAddMeter);
    }

    handleSubmit(event) {        
        event.preventDefault();
        this.addMeter();
    }

    onDateChange(e) {
        this.setState({
            dateOfInstallation: e.target.value
        }); 
    }

    onReadingChange(e) {
        this.setState({
            initialReading: e.target.value
        });
    }

    onNameChange(e) {
        this.setState({
            meterName: e.target.value
        });
    }

    render() {
        return (
            <form onSubmit={this.handleSubmit}>
            <div>
                <fieldset>
                <legend>ElectricMeter</legend>
                <div className="editor-label">
                    Meter Type {'   :   '}
                </div>
                <div className="editor-field">
                    <select>
                    <option key="0" value="0">SubMeter </option>)
                    </select>
                </div>
                <div className="editor-label">
                    Name {'   :   '}
                </div>
                <div className="editor-field">
                    <input type="text" id="txtMeterName" required onChange={this.onNameChange}/>
                </div>
                <div className="editor-label">
                    Date Of Meter Installed  {'   :   '}
                </div>
                <div className="editor-field">
                    <input type="date" id="txtDateOfMeterInstalled" required onChange={this.onDateChange} />
                </div>
                <div className="editor-label">
                    Initial Reading
                </div>
                <div className="editor-field">
                    <input type="number" id="txtInitialReading" required onChange={this.onReadingChange}/>
                </div>
                <p className="button-margin">
                    <input type="submit" className="btn btn-lg btn-primary" value="Add Meter" onSubmit={this.handleSubmit} />
                </p>
                </fieldset>                   
        </div>
        </form>
        );
    }
}

React.render(
    <ElectricMeterControl />,
    document.getElementById('ElectricMeterContainer')
);
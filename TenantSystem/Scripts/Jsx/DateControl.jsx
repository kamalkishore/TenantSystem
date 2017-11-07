class DateControl extends React.Component {
    
    onDateChange(data) {
        this.props.onMonthSelect(data);
    }

    render() {
        return (
            <div className="display-generated-bill">
                Select Month  {'   :   '}
            <input type="month" id="txtMonth" required onChange={ev => this.onDateChange(ev.target.value)} />
            </div>
        );
    }
}
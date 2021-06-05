import React, { Component, useState } from 'react';
import { withTranslation } from 'react-i18next';
import { Button, Card, CardBody, CardHeader, Col, FormGroup, Label, Input, Row, Dropdown, DropdownToggle, DropdownMenu, DropdownItem } from 'reactstrap';
import SmartDeviceApi from '../../services/SmartDeviceApi';


const ComboboxProfessional = (props) => {

    const onChange = e => {
        const { name, value } = e.target;
    }
    const {
        current_professional
    } = 0;

    const professionalOptions = [
        {
            label: "WOG",
            value: 1,
        },
        {
            label: "ShoesOn",
            value: 3,
        }
    ];

    return (
        <>
            <div className="form-group">
                <select value={current_professional} onChange={onChange} id="license_type" name="current_professional">
                    {professionalOptions.map((option) => (
                        <option value={option.value}>{option.label}</option>
                    ))}
                </select>
            </div>
        </>
    )
}




class AddData extends Component {

    constructor() {
        super();

        this.addDataHandler = this.addDataHandler.bind(this);
        this.handleSubmit = this.handleSubmit.bind(this);
    }

    componentDidMount() {
        document.title = "Add Data";
    }

    addDataHandler = (data, callback) => SmartDeviceApi.addSmartDeviceData(data, callback);

    handleSubmit = (event) => {
        event.preventDefault();

        var data = {
            enterpriseId: 3,
            airPollution: event.target.elements['airPollution'].value,
            waterPoluttion: event.target.elements['waterPollution'].value,
        };

        this.addDataHandler(data, () => this.props.history.push('/smart-device'));
    }

    render() {
        const { t } = this.props;
        return (
            <div className="animated fadeIn">
                <Row>
                    <Col xs="12" md="7">
                        <Card>
                            <CardHeader>
                                <strong>{t("Add Pollution Data")}</strong>
                            </CardHeader>
                            <CardBody>
                                <form onSubmit={this.handleSubmit} className="form-horizontal">
                                    <FormGroup row>
                                        <Col md="3">
                                            <Label htmlFor="professional">{t("Enterprise Name")}</Label>
                                        </Col>
                                        <Col xs="12" md="9">
                                            <ComboboxProfessional />
                                        </Col>
                                    </FormGroup>

                                    <FormGroup row>
                                        <Col md="3">
                                            <Label htmlFor="airPollution">{t("Air Pollution")}</Label>
                                        </Col>
                                        <Col xs="12" md="9">
                                            <Input type="text" id="airPollution" placeholder="Air pollution" />
                                        </Col>
                                    </FormGroup>
                                    <FormGroup row>
                                        <Col md="3">
                                            <Label htmlFor="waterPollution">{t("Water Pollution")}</Label>
                                        </Col>
                                        <Col xs="12" md="9">
                                            <Input type="text" id="waterPollution" placeholder="Water pollution" />
                                        </Col>
                                    </FormGroup>

                                    
                                    <Button class="btn btn-primary" style={{ marginTop: 20 }} type="submit" color="primary">{t("Submit")}</Button>
                                </form>
                            </CardBody>
                        </Card>
                    </Col>
                </Row>
            </div>
        )
    }
}


export default withTranslation()(AddData);

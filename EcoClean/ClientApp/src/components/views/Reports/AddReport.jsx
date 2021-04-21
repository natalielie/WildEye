import React, { Component, useState } from 'react';
import { withTranslation } from 'react-i18next';
import { Button, Card, CardBody, CardHeader, Col, FormGroup, Label, Input, Row, Dropdown, DropdownToggle, DropdownMenu, DropdownItem } from 'reactstrap';
import ReportsApi from '../../services/ReportsApi';
import EnterpriseApi from '../../services/EnterpriseApi';

const ComboboxEnterprise = (props) => {

    const onChange = e => {
        const { name, value } = e.target;
    }
    const {
        current_enterprise
    } = 0;


    var enterpriseData = EnterpriseApi.getAllEnterprises();

    const professionalOptions = [
        {
            label: "John Smith",
            value: 1,
        },
        {
            label: "Kate Wolson",
            value: 2,
        },
        {
            label: "Anatolii Kompotov",
            value: 3,
        },
        {
            label: "Ahmad Amshanov",
            value: 4,
        },
    ];

    return (
        <>
            <div className="form-group">
                <select value={current_enterprise} onChange={onChange} id="license_type" name="current_enterprise">
                    {enterpriseData.map((option) => (
                        <option value={option.value}>{option.label}</option>
                    ))}
                </select>
            </div>
        </>
    )
}

const ComboboxTax = (props) => {

    const onChange = e => {
        const { name, value } = e.target;
    }
    const {
        current_pet
    } = 0;


    const enterpriseData = EnterpriseApi.getAllEnterprises();

    const petOptions = [
        {
            label: "Twinkle",
            value: 1,
        },
        {
            label: "Jim",
            value: 2,
        },
        {
            label: "Cinnabon",
            value: 3,
        },
    ];

    return (
        <>
            <div className="form-group">
                <select value={current_pet} onChange={onChange} id="license_type" name="current_pet">
                    {petOptions.map((option) => (
                        <option value={option.value}>{option.label}</option>
                    ))}
                </select>
            </div>
        </>
    )
}


class AddReport extends Component {

    constructor() {
        super();

        this.addReportHandler = this.addReportHandler.bind(this);
        this.handleSubmit = this.handleSubmit.bind(this);
    }

    componentDidMount() {
        document.title = "Add Assignment";

        var taxData = ReportsApi.getAllTaxes();
    }


    addReportHandler = (report, callback) => ReportsApi.createReport(report, callback);

    handleSubmit = (event) => {
        event.preventDefault();

        var data = {
            name: event.target.elements['enterprise'].value,
            tax: event.target.elements['tax'].value,
            comment: event.target.elements['comment'].value
        };

        this.addReportHandler(data, () => this.props.history.push('/reports'));
    }

    render() {
        const { t } = this.props;
        return (
            <div className="animated fadeIn">
                <Row>
                    <Col xs="12" md="7">
                        <Card>
                            <CardHeader>
                                <strong>{t("Add Report")}</strong>
                            </CardHeader>
                            <CardBody>
                                <form onSubmit={this.handleSubmit} className="form-horizontal">
                                    <FormGroup row>
                                        <Col md="3">
                                            <Label htmlFor="enterprise">{t("Enterprise Name")}</Label>
                                        </Col>
                                        <Col xs="12" md="9">
                                            <ComboboxEnterprise />
                                        </Col>
                                    </FormGroup>

                                    <FormGroup row>
                                        <Col md="3">
                                            <Label htmlFor="tax">{t("Tax")}</Label>
                                        </Col>
                                        <Col xs="12" md="9">
                                            <ComboboxTax />
                                        </Col>
                                    </FormGroup>

                                    <FormGroup row>
                                        <Col md="3">
                                            <Label htmlFor="comment">Comment</Label>
                                        </Col>
                                        <Col xs="12" md="9">
                                            <Input type="text" id="comment" placeholder="Comment" />
                                        </Col>
                                    </FormGroup>
                                    
                                    <Button type="submit" color="primary">{t("Submit")}</Button>
                                </form>
                            </CardBody>
                        </Card>
                    </Col>
                </Row>
            </div>
        )
    }
}


export default withTranslation()(AddReport);

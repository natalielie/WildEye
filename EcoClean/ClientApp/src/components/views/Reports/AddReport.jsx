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

    const enterpriseOptions = [
        {
            label: "WOG",
            value: 6,
        },
        {
            label: "Shoes On",
            value: 8,
        },
    ];

    return (
        <>
            <div className="form-group">
                <select value={current_enterprise} onChange={onChange} id="license_type" name="current_enterprise">
                    {enterpriseOptions.map((option) => (
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
        document.title = "Add Report";

    }


    addReportHandler = (report, callback) => ReportsApi.createReport(report, callback);

    handleSubmit = (event) => {
        event.preventDefault();

        var data = {
            enterpriseId: 3,
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
                                            <Label htmlFor="enterpriseId">{t("Enterprise Name")}</Label>
                                        </Col>
                                        <Col xs="12" md="9">
                                            <ComboboxEnterprise />
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

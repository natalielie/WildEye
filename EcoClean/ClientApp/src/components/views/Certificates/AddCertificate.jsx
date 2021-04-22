import React, { Component, useState } from 'react';
import { withTranslation } from 'react-i18next';
import { Button, Card, CardBody, CardHeader, Col, FormGroup, Label, Input, Row, Dropdown, DropdownToggle, DropdownMenu, DropdownItem } from 'reactstrap';
import CertificatesApi from '../../services/CertificatesApi';


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
            label: "Johnny's",
            value: 2,
        },
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



class AddCertificate extends Component {

    constructor() {
        super();

        this.addCertificateHandler = this.addCertificateHandler.bind(this);
        this.handleSubmit = this.handleSubmit.bind(this);
    }

    componentDidMount() {
        document.title = "Add Certificate";
    }

    addCertificateHandler = (appointment, callback) => CertificatesApi.createCertificate(appointment, callback);

    handleSubmit = (event) => {
        event.preventDefault();

        var data = {
            enterpriseId: 1,
        };

        this.addCertificateHandler(data, () => this.props.history.push('/certificates'));
    }

    render() {
        const { t } = this.props;
        return (
            <div className="animated fadeIn">
                <Row>
                    <Col xs="12" md="7">
                        <Card>
                            <CardHeader>
                                <strong>{t("Add Certificate")}</strong>
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


export default withTranslation()(AddCertificate);

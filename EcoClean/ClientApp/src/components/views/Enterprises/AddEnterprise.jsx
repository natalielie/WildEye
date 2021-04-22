import React, { Component, useState } from 'react';
import { withTranslation } from 'react-i18next';
import { Button, Card, CardBody, CardHeader, Col, FormGroup, Label, Input, Row, Dropdown, DropdownToggle, DropdownMenu, DropdownItem } from 'reactstrap';
import EnterpriseApi from '../../services/EnterpriseApi';



class AddEnterprise extends Component {

    constructor() {
        super();

        this.addEnterpriseHandler = this.addEnterpriseHandler.bind(this);
        this.handleSubmit = this.handleSubmit.bind(this);
    }

    componentDidMount() {
        document.title = "Add Enterprise";
    }

    addEnterpriseHandler = (enterprise, callback) => EnterpriseApi.addEnterprise(enterprise, callback);

    handleSubmit = (event) => {
        event.preventDefault();

        var data = {
            name: event.target.elements['name'].value,
            kind: event.target.elements['kind'].value,
            phoneNumber: event.target.elements['phoneNumber'].value,
            product: event.target.elements['product'].value,
            address: event.target.elements['address'].value,
        };

        this.addEnterpriseHandler(data, () => this.props.history.push('/enterprises'));
    }


    render() {
        const { t } = this.props;
        return (
            <div className="animated fadeIn">
                <Row>
                    <Col xs="12" md="7">
                        <Card>
                            <CardHeader>
                                <strong>{t("Add Enterprise")}</strong>
                            </CardHeader>
                            <CardBody>
                                <form onSubmit={this.handleSubmit} className="form-horizontal">
                                    <FormGroup row>
                                        <Col md="3">
                                            <Label htmlFor="name">{t("Name")}</Label>
                                        </Col>
                                        <Col xs="12" md="9">
                                            <Input type="text" id="name" required placeholder="Enterprise Name" />
                                        </Col>
                                    </FormGroup>
                                    <FormGroup row>
                                        <Col md="3">
                                            <Label htmlFor="kind">{t("Kind")}</Label>
                                        </Col>
                                        <Col xs="12" md="9">
                                            <Input type="text" id="kind" placeholder="Kind" />
                                        </Col>
                                    </FormGroup>
                                    <FormGroup row>
                                        <Col md="3">
                                            <Label htmlFor="phoneNumber">{t("Phone Number")}</Label>
                                        </Col>
                                        <Col xs="12" md="9">
                                            <Input type="text" id="phoneNumber" placeholder="Phone Number" />
                                        </Col>
                                    </FormGroup>
                                    <FormGroup row>
                                        <Col md="3">
                                            <Label htmlFor="product">{t("Product")}</Label>
                                        </Col>
                                        <Col xs="12" md="9">
                                            <Input type="text" id="product" placeholder="Product" />
                                        </Col>
                                    </FormGroup>
                                    <FormGroup row>
                                        <Col md="3">
                                            <Label htmlFor="address">{t("Address")}</Label>
                                        </Col>
                                        <Col xs="12" md="9">
                                            <Input type="text" id="address" placeholder="Address" />
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


export default withTranslation()(AddEnterprise);

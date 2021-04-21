import React, { Component, useState } from 'react';
import { withTranslation } from 'react-i18next';
import { Button, Card, CardBody, CardHeader, Col, FormGroup, Label, Input, Row, Dropdown, DropdownToggle, DropdownMenu, DropdownItem } from 'reactstrap';
import EnterpriseApi from '../../services/EnterpriseApi';


const ComboboxProduct = (props) => {

    const onChange = e => {
        const { name, value } = e.target;
    }
    const {
        current_prod
    } = 0;

    const productOptions = [
        {
            label: "Food",
            value: 1,
        },
        {
            label: "Fuel",
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
        {
            label: "Ahmad Amshanov",
            value: 4,
        },
    ];

    return (
        <>
            <div className="form-group">
                <select value={current_prod} onChange={onChange} id="license_type" name="current_prod">
                    {productOptions.map((option) => (
                        <option value={option.value}>{option.label}</option>
                    ))}
                </select>
            </div>
        </>
    )
}

const ComboboxPet = (props) => {

    const onChange = e => {
        const { name, value } = e.target;
    }
    const {
        current_pet
    } = 0;

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
            pet: event.target.elements['kind'].value,
            pet: event.target.elements['phoneNumber'].value,
            name: event.target.elements['product'].value,
            name: event.target.elements['address'].value,
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
                                            <Input type="text" id="name" required placeholder="name" />
                                        </Col>
                                    </FormGroup>
                                    <FormGroup row>
                                        <Col md="3">
                                            <Label htmlFor="kind">{t("Kind")}</Label>
                                        </Col>
                                        <Col xs="12" md="9">
                                            <Input type="text" id="kind" placeholder="kind" />
                                        </Col>
                                    </FormGroup>
                                    <FormGroup row>
                                        <Col md="3">
                                            <Label htmlFor="phoneNumber">{t("Phone Number")}</Label>
                                        </Col>
                                        <Col xs="12" md="9">
                                            <Input type="text" id="phoneNumber" placeholder="phoneNumber" />
                                        </Col>
                                    </FormGroup>
                                    <FormGroup row>
                                        <Col md="3">
                                            <Label htmlFor="product">{t("Product")}</Label>
                                        </Col>
                                        <Col xs="12" md="9">
                                            <Input type="text" id="product" placeholder="product" />
                                        </Col>
                                    </FormGroup>
                                    <FormGroup row>
                                        <Col md="3">
                                            <Label htmlFor="address">{t("Address")}</Label>
                                        </Col>
                                        <Col xs="12" md="9">
                                            <Input type="text" id="address" placeholder="address" />
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

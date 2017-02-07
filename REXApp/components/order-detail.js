import React, { Component } from 'react';
import { View, Text, TextInput, Picker, Navigator, ScrollView, Linking, StyleSheet, TouchableOpacity } from 'react-native';
import Api from '../api/api';
import DatePicker from '../controls/date-picker.android';

var PickerItem = Picker.Item;

class OrderDetail extends Component {
    constructor(props) {
        super(props);
        this.state = {
            order: null,
            districts: [],
            timesInDay:[],
            riceTypes:[],
            district:null,
            timeADay:null,
            riceType:null
        }

        this.GetNewOrder();
        this.GetListItems();
    }

    GetNewOrder() {
        Api.getOrderDefaultNewContact().then((res) => {
            this.setState({
                order: res,
                district:res.contact.districtId,
            });
        })
    }

    GetListItems() {
        Api.getAllListItems().then((res) => {
            debugger;
            this.setState({
                districts: res.districts,
                timesInDay:res.timesInDay,
                riceTypes:res.riceTypes,
                
            });
        });
    }

    render() {
        if (this.state.order != null) {
            return (
                <View>
                    <ScrollView>
                        <View style={styles.row}>
                            <Text style={styles.label}>Contact name:</Text>
                            <TextInput style={styles.input} value={this.state.order.contact.name} />
                        </View>
                        <View style={styles.row}>
                            <Text style={styles.label}>Facebook name:</Text>
                            <TextInput style={styles.input} value={this.state.order.contact.faceBookName} />
                        </View>
                        <View style={styles.row}>
                            <Text style={styles.label}>Phone 1:</Text>
                            <TextInput style={styles.input} value={this.state.order.contact.phone1} />
                        </View>
                        <View style={styles.row}>
                            <Text style={styles.label}>Phone 2:</Text>
                            <TextInput style={styles.input} value={this.state.order.contact.phone2} />
                        </View>
                        <View style={styles.row}>
                            <Text style={styles.label}>Address:</Text>
                            <TextInput style={styles.input} value={this.state.order.contact.address} />
                        </View>
                        <View style={styles.row}>
                            <Text style={styles.label}>District:</Text>
                            <Picker style={styles.input} 
                                selectedValue={this.state.district}
                                onValueChange={(dist) => (this.setState({ district: dist }))}  >
                                {this.state.districts.map((s, i) => {
                                    return <PickerItem
                                        key={i}
                                        value={s.id}
                                        label={s.name} />
                                })}
                            </Picker>
                        </View>
                        <View style={styles.row} >
                            <Text style={styles.label}>Time receiving:</Text>
                            <Picker style={styles.input} 
                                selectedValue={this.state.timeADay}
                                onValueChange={(dist) => (this.setState({ timeADay: dist }))}  >
                                {this.state.timesInDay.map((s, i) => {
                                    debugger;
                                    return <PickerItem
                                        key={i}
                                        value={s.id}
                                        label={s.timeInDay} />
                                })}
                            </Picker>
                        </View>
                        <View style={styles.row} >
                            <Text style={styles.label}>Ship date:</Text>
                            <DatePicker style={styles.input}  />
                        </View>
                    </ScrollView>
                </View>
            );
        }
        else {
            return (
                <View>
                    <Text>Loading...</Text>
                </View>
            )

        }

    }
}

const styles = StyleSheet.create({
    detail: {
        paddingTop: 60
    },
    row: {
        flexDirection: 'row',
        paddingLeft: 5
    },
    button: {
        padding: 10,
        backgroundColor: 'gray',
        marginBottom: 10,
    },
    label: {
        flex: 0.25
    },
    text: {
        color: 'white',
    },
    input: {
        flex: 0.75
    }
});

export default OrderDetail;
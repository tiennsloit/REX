import React, { Component } from 'react';
import { Button, View, Text, TextInput, Picker, Navigator, ScrollView, Linking, StyleSheet, TouchableOpacity } from 'react-native';
import Api from '../api/api';
import DatePicker from '../controls/date-picker.android';
import {Actions} from 'react-native-router-flux';
import update from 'immutability-helper';
var PickerItem = Picker.Item;

class OrderDetail extends Component {
    constructor(props) {
        super(props);
        this.state = {
            order: null,
        }

        this.GetNewOrder();
        this.GetListItems();
    }

    GetNewOrder() {
        Api.getOrderDefaultNewContact().then((res) => {
            this.setState({
                order: res
            });
        })
    }

    GetListItems() {
        Api.getAllListItems().then((res) => {
           
            this.setState({
                districts: res.districts,
                timesInDay: res.timesInDay,
                riceTypes: res.riceTypes,

            });
        });
    }

    saveOrder() {
        Api.saveOrder(this.state.order);
        Actions.contactList();
    }

    updateText(target)
    {
      this.setState({order:update(this.state, target).order}) 
    }

    render() {
        if (this.state.order != null) {
            return (
                <View>
                    <ScrollView>
                        <View style={styles.row}>
                            <Text style={styles.label}>Contact name:</Text>
                            <TextInput style={styles.input} value={this.state.order.contact.name} onChangeText={(value)=>this.updateText({order:{contact:{name:{$set:value}}}})} />
                        </View>
                        <View style={styles.row}>
                            <Text style={styles.label}>Facebook name:</Text>
                            <TextInput style={styles.input} value={this.state.order.contact.faceBookName} onChangeText={(value)=>this.updateText({order:{contact:{faceBookName:{$set:value}}}})}/>
                        </View>
                        <View style={styles.row}>
                            <Text style={styles.label}>Phone 1:</Text>
                            <TextInput keyboardType="phone-pad"  style={styles.input} value={this.state.order.contact.phone1} onChangeText={(value)=>this.updateText({order:{contact:{phone1:{$set:value}}}})}/>
                        </View>
                        <View style={styles.row}>
                            <Text style={styles.label}>Phone 2:</Text>
                            <TextInput keyboardType="phone-pad" style={styles.input} value={this.state.order.contact.phone2} onChangeText={(value)=>this.updateText({order:{contact:{phone2:{$set:value}}}})}/>
                        </View>
                        <View style={styles.row}>
                            <Text style={styles.label}>Address:</Text>
                            <TextInput style={styles.input} value={this.state.order.contact.address} onChangeText={(value)=>this.updateText({order:{contact:{address:{$set:value}}}})}/>
                        </View>
                        <View style={styles.row}>
                            <Text style={styles.label}>District:</Text>
                            <Picker style={styles.inputPicker}
                                selectedValue={this.state.order.contact.districtId}
                                onValueChange={(dist) => this.updateText({order:{contact:{districtId:{$set:dist}}}})}  >
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
                            <Picker style={styles.inputPicker}
                                selectedValue={this.state.order.contact.timeCanReceivedId}
                                onValueChange={(time) => this.updateText({order:{contact:{timeCanReceivedId:{$set:time}}}})}  >
                                {this.state.timesInDay.map((s, i) => {
                                   
                                    return <PickerItem
                                        key={i}
                                        value={s.id}
                                        label={s.timeInDay} />
                                })}
                            </Picker>
                        </View>
                        <View style={styles.row} >
                            <Text style={styles.label}>Rice Type:</Text>
                            <Picker style={styles.inputPicker}
                                selectedValue={this.state.order.riceType1Id}
                                onValueChange={(rice) => this.updateText({order:{riceType1Id:{$set:rice}}})}  >
                                {this.state.riceTypes.map((s, i) => {
                                   
                                    return <PickerItem
                                        key={i}
                                        value={s.id}
                                        label={s.name} />
                                })}
                            </Picker>
                        </View>
                        <View style={styles.row} >
                            <Text style={styles.label}>Ship date:</Text>
                            <DatePicker style={styles.input} onValueChange={(newDate) => this.updateText({order:{dateShipped:{$set:newDate}}})} />
                        </View>
                        <View style={styles.row}>
                            <Text style={styles.label}>Weight:</Text>
                            <TextInput keyboardType="numeric" style={styles.input} value={this.state.order.weight} onChangeText={(value) => this.updateText({order:{weight:{$set:value}}})}/>
                        </View>
                        <View style={styles.row}>
                            <Text style={styles.label}>Price:</Text>
                            <TextInput keyboardType="numeric" style={styles.input} value={this.state.order.price} onChangeText={(value) => this.updateText({order:{price:{$set:value}}})}/>
                        </View>
                        <View style={styles.row}>
                            <Text style={styles.label}>Surcharge:</Text>
                            <TextInput keyboardType="numeric" style={styles.input} value={this.state.order.surcharge} onChangeText={(value) => this.updateText({order:{surcharge:{$set:value}}})}/>
                        </View>
                        <View style={styles.row}>
                            <Text style={styles.label}>Amount To Receive:</Text>
                            <TextInput keyboardType="numeric" style={styles.input} value={this.state.order.amountToReceived} onChangeText={(value) => this.updateText({order:{amountToReceived:{$set:value}}})}/>
                        </View>
                        <View style={styles.row}>
                            <Text style={styles.label}>Cover price:</Text>
                            <TextInput keyboardType="numeric" style={styles.input} value={this.state.order.coverPrice} onChangeText={(value) => this.updateText({order:{coverPrice:{$set:value}}})}/>
                        </View>
                        <View style={styles.row}>
                            <Text style={styles.label}>Promo price:</Text>
                            <TextInput keyboardType="numeric" style={styles.input} value={this.state.order.promoPrice} onChangeText={(value) => this.updateText({order:{promoPrice:{$set:value}}})}/>
                        </View>
                        <View style={styles.row}>
                            <Text style={styles.label}>Total Price:</Text>
                            <TextInput keyboardType="numeric" style={styles.input} value={this.state.order.totalPrice} onChangeText={(value) => this.updateText({order:{totalPrice:{$set:value}}})}/>
                        </View>
                        <View style={styles.row}>
                            <Text style={styles.label}>Ship Fee:</Text>
                            <TextInput keyboardType="numeric" style={styles.input} value={this.state.order.shipFee} onChangeText={(value) => this.updateText({order:{shipFee:{$set:value}}})}/>
                        </View>
                        <View style={styles.row}>
                            <Text style={styles.label}>Other Fee:</Text>
                            <TextInput keyboardType="numeric" style={styles.input} value={this.state.order.otherFee} onChangeText={(value) => this.updateText({order:{otherFee:{$set:value}}})}/>
                        </View>
                        <View style={styles.row}>
                            <Text style={styles.label}>Profit:</Text>
                            <TextInput keyboardType="numeric" style={styles.input} value={this.state.order.profit} onChangeText={(value) => this.updateText({order:{profit:{$set:value}}})}/>
                        </View>
                        <View style={styles.row}>
                            <Text style={styles.label}>Paid:</Text>
                            <TextInput keyboardType="numeric" style={styles.input} value={this.state.order.paid} onChangeText={(value) => this.updateText({order:{paid:{$set:value}}})} />
                        </View>
                        <View style={styles.row}>
                            <Text style={styles.label}>Received:</Text>
                            <TextInput keyboardType="numeric" style={styles.input} value={this.state.order.received} onChangeText={(value) => this.updateText({order:{received:{$set:value}}})}/>
                        </View>
                        <View style={styles.row}>
                            <Text style={styles.label}>IsNew:</Text>
                            <Text style={styles.input}>{this.state.order.isNew}</Text>
                        </View>
                        <Button onPress={() => this.saveOrder()} title="Save Order" />
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
        flex: 0.35,
        paddingTop:35
    },
    text: {
        color: 'white',
    },
    input: {
        flex: 0.65
    },
    inputPicker: {
        flex: 0.65,
        paddingTop:40
    }
});

export default OrderDetail;
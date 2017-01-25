import React, { Component } from 'react';
import { View, Text, TextInput, Navigator, ScrollView, Linking, StyleSheet, TouchableOpacity } from 'react-native';

class ContactDetail extends Component {
  static get defaultProps() {
    return {
       title:'test'
    };
  }

  toFaceBook(url) {
    Linking.canOpenURL('https://www.facebook.com').then(supported => {
      if (supported) {
        Linking.openURL('https://www.facebook.com');
      } else {
        console.log('Don\'t know how to open URI: https://www.facebook.com');
      }
    });
  }

  callPhone1() {
    Linking.canOpenURL('tel:+840902156066').then(supported => {
      if (supported) {
        Linking.openURL('tel:+840902156066');
      } else {
        console.log('Don\'t know how to open URI: tel:+840902156066');
      }
    });
  }

  render() {

    return (
      <View style={styles.detail}>
        <ScrollView>
          <View style={styles.row}>
            <Text style={styles.label}>Tên:</Text>
            <TextInput style={styles.input} value={this.props.name} />
          </View>
          <View style={styles.row}>
            <Text style={styles.label}>Facebook:</Text>
            <TouchableOpacity style={styles.input}
              onPress={() => { this.toFaceBook() } }>
              <View style={styles.button}>
                <Text style={styles.text}>Mở {this.props.faceBookName}</Text>
              </View>
            </TouchableOpacity >
          </View>
          <View style={styles.row}>
            <Text style={styles.label}>Phone1:</Text>
            <TouchableOpacity style={styles.input} onPress={() => this.callPhone1(this.props.phone1)}>
              <View style={styles.button}>
                <Text style={styles.text}>Gọi {this.props.phone1}</Text>
              </View>
            </TouchableOpacity >
          </View>
          <View style={styles.row}>
            <Text style={styles.label}>Phone2:</Text>
            <TouchableOpacity style={styles.input} onPress={() => this.callPhone1(this.props.phone2)}>
              <View style={styles.button}>
                <Text style={styles.text}>Gọi {this.props.phone2}</Text>
              </View>
            </TouchableOpacity >
          </View>
          <View style={styles.row}>
            <Text style={styles.label}>Địa chỉ:</Text>
            <TextInput style={styles.input} value={this.props.address} multiline={true} />
          </View>
          <View style={styles.row}>
            <Text style={styles.label}>Gạo 1:</Text>
            <TextInput style={styles.input} value={this.props.riceType1} />
          </View>
          <View style={styles.row}>
            <Text style={styles.label}>Gạo 2:</Text>
            <TextInput style={styles.input} value={this.props.riceType2} />
          </View>
          <View style={styles.row}>
            <Text style={styles.label}>Số ký:</Text>
            <TextInput style={styles.input} value={this.props.weight} />
          </View>
          <View style={styles.row}>
            <Text style={styles.label}>Tiền nhận:</Text>
            <TextInput style={styles.input} value={this.props.amountToReceived} />
          </View>
          <View style={styles.row}>
            <Text style={styles.label}>Ngày giao:</Text>
            <TextInput style={styles.input} value={this.props.dateShipped} />
          </View>
          <View style={styles.row}>
            <Text style={styles.label}>Giờ giao:</Text>
            <TextInput style={styles.input} value={this.props.timeCanReceived} />
          </View>
        </ScrollView>
      </View>

    )
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

export default ContactDetail;
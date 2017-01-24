/**
 * Sample React Native App
 * https://github.com/facebook/react-native
 * @flow
 */
import React, { Component } from 'react';
import ReactNative from 'react-native';
import DatePicker from './controls/date-picker.android.js';
import Api from './api/api.js';
const onButtonPress = () => {
  Alert.alert('Button has been pressed!');
};
import {
  AppRegistry,
  StyleSheet,
  Text,
  View,
  Alert,
  Button,
  ListView,

  Picker
} from 'react-native';

const ds = new ListView.DataSource({ rowHasChanged: (r1, r2) => r1 !== r2 });

export default class REXApp extends Component {
  constructor(props) {
    super(props);

    this.state = {
      items: [],
      hasData: "no data",
      mainListData: ds.cloneWithRows([])
    }

  }

  fetchData() {

    Api.getDrive().then(res => {
      this.setState({
        items: res,
        hasData: "has dataq",
        mainListData: ds.cloneWithRows(res)
      });
    });
  }


  render() {

    return (
      <View>
        <ListView style={styles.container}
          dataSource={this.state.mainListData}
          renderRow={(rowData) =><View>
            <Text style={styles.listViewItem}>{rowData.name}</Text>
            
            </View> 
          }
          renderSeparator={(sectionId, rowId) => <View key={rowId} style={styles.separator} />}
          contentContainerStyle={styles.listView}
          />

        <Button onPress={() => this.fetchData()} title="Fetch data" />
      </View>
      // <View >
      //  <DatePicker hellotest="abc" />
      //  <Button onPress={()=>this.fetchData()} title="Fetch data"/>
      //  <Text>{this.state.hasData}</Text>
      // </View>
    );
  }
}

const styles = StyleSheet.create({
  picker: {
    width: 100,
  },
  listView: {
    justifyContent: 'center',
  },
  container: {
    padding: 12,
  },
  welcome: {
    fontSize: 20,
    textAlign: 'center',
    margin: 10,
  },
  
  text: {
    color: 'black',
  },
  buttonCall:{
    width:10,
    textAlign:'right'
  },
  listViewItem: {
    height: 40,
    marginLeft: 12,
    fontSize: 16,
    paddingTop:10
  },
  separator: {
    flex: 1,
    height: StyleSheet.hairlineWidth,
    backgroundColor: '#8E8E8E',
  },
});

AppRegistry.registerComponent('REXApp', () => REXApp);

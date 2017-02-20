import React, { Component } from 'react';
import { View, Text, StyleSheet, TouchableHighlight } from 'react-native';

class StatusModal  extends Component {
    constructor(props) {
    super(props)
    // set state with passed in props
    this.state = {
      message: props.error,
      hide: props.hide,
    }
    // bind functions
    this.dismissModal = this.dismissModal.bind(this)
  }

  dismissModal() {
    this.setState({hide: true})
  }

  // show or hide Modal based on 'hide' prop
  render() {
    if(this.state.hide){
      return (
        <View>
        </View>
      )
    } else {
        return (
          <TouchableHighlight  onPress={this.dismissModal}>
            <Text >{this.state.message}</Text>
          </TouchableHighlight>
        )
      }
  }
}

export default StatusModal ;
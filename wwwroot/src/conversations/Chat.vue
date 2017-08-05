<template>
  <div>
    <loader :loading="isLoading"></loader>
    <div v-if="!isLoading">
      <header>
        <h1 class="headline primary--text">Chat</h1>
      </header>
      <v-card class="chat-wrapper">
        <v-layout row wrap>
          <v-flex xs12 sm4 class="conversations">
            <ul>
              <li v-for="item in conversations"
                  :key="item.conversationId"
                  :class="{'active': item.conversationId === selectedConversation.conversationId}"
                  @click="selectedConversation = item">
                <v-avatar>
                  <img src="../assets/images/avatar.jpg" alt="Avatar">
                  <span>{{ item.senderFirstName + ' ' + item.senderLastName }}</span>
                </v-avatar>
              </li>
            </ul>
          </v-flex>
          <v-flex xs12 sm8 v-if="!selectedConversation || !selectedConversation.messages || !selectedConversation.messages.length" class="no-messages">
            There are no messages to display here.
          </v-flex>
          <v-flex xs12 sm8 class="chat grey lighten-4">
            <ul v-if="selectedConversation && selectedConversation.messages && selectedConversation.messages.length > 0" class="scroll" v-chat-scroll>
              <li v-for="message in selectedConversation.messages"
                  :key="message.id"
                  :class="{'sender': selectedConversation.senderId === message.senderId, 'receiver': selectedConversation.senderId === message.receiverId}">
                  <div class="message">
                    {{ message.message }}
                    <div class="sent">
                      <v-icon>alarm</v-icon>
                      <time :datetime="message.sent">{{ message.sent | formatDateTime }}</time>
                    </div>
                  </div>
              </li>
            </ul>
            <div class="type-message">
              <text-field v-model="currentMessage"
                          @keyenter="sendMessage(currentMessage)"
                          :disabled="isSending"
                          placeholder="Type a message"
                          hint="Press enter or return to send">
              </text-field>
            </div>
          </v-flex>
        </v-layout>
        <v-dialog v-model="dialog"
                  v-if="contacts && contacts.length"
                  lazy absolute>
          <v-btn class="blue darken-2 action-button"
                 slot="activator"
                 absolute dark fab top right>
            <v-icon>add</v-icon>
          </v-btn>
          <v-list>
            <v-list-tile avatar v-for="contact in contacts" v-bind:key="contact.id">
              <v-list-tile-avatar>
                <img src="../assets/images/avatar.jpg"/>
              </v-list-tile-avatar>
              <v-list-tile-content>
                <v-list-tile-title @click="selectContact(contact)">
                  {{ contact.firstName + ' ' + contact.lastName }}
                </v-list-tile-title>
              </v-list-tile-content>
            </v-list-tile>
          </v-list>
        </v-dialog>
      </v-card>
    </div>
  </div>
</template>

<script>
import { mapState } from 'vuex'
import utils from 'utils'

export default {
  name: 'chat',
  data () {
    return {
      dialog: false,
      currentMessage: null,
      isLoading: false,
      isSending: false,
      conversations: [],
      contacts: [],
      selectedConversation: {}
    }
  },
  created () {
    this.isLoading = true
    this.$http.get('/api/conversation')
      .then(response => {
        let data = response.data
        this.conversations = data.conversations
        this.contacts = data.contacts
      })
      .finally(() => {
        this.isLoading = false
        if (this.conversations) {
          this.selectedConversation = this.conversations[0]
        }
      })
  },
  methods: {
    selectContact (contact) {
      this.dialog = false
      this.$http.post('/api/conversation/new', { userId: contact.userId })
        .then(response => {
          this.conversations.push(response.data)
        })
    },
    sendMessage (message) {
      if (!message) {
        return
      }

      let conversationMessage = {
        conversationId: this.selectedConversation.conversationId,
        senderId: this.selectedConversation.senderId,
        message
      }

      this.$http.post('/api/conversation', conversationMessage)
        .then(response => {
          this.selectedConversation.messages.push(response.data)
        })
        .catch(response => {
          let validationResult = utils.getFormValidationErrors(response)
          validationResult.errors.forEach(validationError => {
            console.log('ERROR', validationError.key, validationError.messages[0], 'required')
          })
        })
        .finally(() => {
          this.isLoading = false
          this.currentMessage = ''

          if (this.conversations) {
            this.selectedConversation = this.conversations[0]
          }
        })
    }
  },
  computed: {
    ...mapState({
      permissions: state => state.permissions
    })
  }
}
</script>

<style lang="scss" scoped>
  .chat-wrapper {
    height: 70vh!important;
  }
  .conversations {
    border-right: 1px solid #eaeaea;
    padding-right: 0;
    ul {
      list-style: none;
      padding-left: 0;
      li {
        cursor: pointer;
        padding: 1rem;
        border-bottom: 1px solid #eaeaea;
        margin-left: 3px;
        &.active {
          background-color: #e9ebeb;
        }
      }
    }
  }
  .chat {
    padding: 0;
    overflow: hidden;
    ul {
      list-style: none;
      height: calc(70vh - 117px);
      overflow-y: scroll;
      overflow-x: hidden;
      padding: 0 1rem;
      li {
        display: flex;
        margin: 1rem 0;
        .message {
          order: 1;
          box-shadow: -1px 2px 0px #D4D4D4;
          background: #fff;
          min-width: 50px;
          max-width: 50%;
          padding: 10px;
          border-radius: 2px;

          .sent {
            color: rgba(0, 0, 0, .45);
            margin-top: 0.5rem;
            font-size: 0.75rem;
            i {
              font-size: 0.75rem;
            }
          }
        }
        &.receiver {
          justify-content: flex-end;
          align-items: flex-end;

          .message {
            background-color: #b3e5fc;
            box-shadow: 1px 2px 0px #D4D4D4;

            .sent {
              text-align: right;
            }
          }
        }
      }
    }
    .type-message {
      background-color: #ffffff;
      border-top: 1px solid #eaeaea;
      padding: 1rem;

      textarea {
        height: 82px;
      }
    }
  }
  .avatar {
    text-align: left;
    justify-content: flex-start;
    img {
      border: 1px solid #eaeaea;
    }
    span {
      margin-left: 1rem;
    }
  }
  .no-messages {
    height: 70vh;
    justify-content: center;
    align-items: center;
    display: flex;
    height: 100%;
  }

  textarea {
    width: 100%;
    height: 100%;
  }

  .scroll {
    &::-webkit-scrollbar {
      width: 5px;
    }

    &::-webkit-scrollbar-track {
      background: #ddd;
    }

    &::-webkit-scrollbar-thumb {
      background: #666;
    }
  }

  .action-button {
    border-radius: 50%;
  }
</style>

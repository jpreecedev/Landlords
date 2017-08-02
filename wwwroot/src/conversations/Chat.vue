<template>
  <div>
    <loader :loading="isLoading"></loader>
    <div v-if="!isLoading">
      <header>
        <h1 class="headline primary--text">Chat</h1>
      </header>
      <v-card class="chat-wrapper">
        <v-layout row wrap></v-layout>
          <v-flex xs12 md4 class="conversations">
            <ul>
              <li v-for="item in conversations"
                  :key="item.conversationId"
                  :class="{'active': item.conversationId === selectedConversation.conversationId}"
                  @click="selectedConversation = item">
                <v-avatar>
                  <img src="../assets/images/avatar.jpg" alt="Avatar">
                  <span>{{ item.landlordFirstName + ' ' + item.landlordLastName }}</span>
                </v-avatar>
              </li>
            </ul>
          </v-flex>
          <v-flex xs12 md8 v-if="!selectedConversation || !selectedConversation.messages.length === 0" class="no-messages">
            There are no messages to display here.
          </v-flex>
          <v-flex xs12 md8 class="chat grey lighten-4" v-if="selectedConversation">
            <ul v-if="selectedConversation.messages.length > 0" class="scroll" v-chat-scroll>
              <li v-for="message in selectedConversation.messages"
                  :key="message.id"
                  :class="{'sender': message.senderId === message.tenantId, 'receiver': message.senderId === message.landlordId}">
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
        <v-btn absolute dark fab top right class="pink action-button">
          <v-icon>add</v-icon>
        </v-btn>
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
      currentMessage: null,
      isLoading: false,
      isSending: false,
      conversations: [],
      selectedConversation: {}
    }
  },
  created () {
    this.isLoading = true
    this.$http.get('/api/conversation')
      .then(response => {
        this.conversations = response.data
      })
      .finally(() => {
        this.isLoading = false

        if (this.conversations) {
          this.selectedConversation = this.conversations[0]
        }
      })
  },
  methods: {
    sendMessage (message) {
      let conversationMessage = {
        conversationId: this.selectedConversation.conversationId,
        message,
        tenantId: null,
        landlordId: null,
        senderId: null,
        sent: new Date()
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

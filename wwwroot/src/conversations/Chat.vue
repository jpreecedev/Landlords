<template>
  <div>
    <loader :loading="isLoading"></loader>
    <div v-if="!isLoading">
      <header>
        <h1 class="headline primary--text">Chat</h1>
      </header>
      <v-card class="chat-wrapper">
        <v-layout row wrap>
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
          <v-flex xs12 md8 class="chat grey lighten-4" :class="{'no-messages': selectedConversation.messages.length === 0}">
                <div class="" v-if="selectedConversation.messages.length === 0">There are no messages to display here.</div>
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
                  <text-field placeholder="Type a message"
                              hint="Press enter or return to send">
                  </text-field>
                </div>
          </v-flex>
        </v-layout>
      </v-card>
    </div>
  </div>
</template>

<script>
import { mapState } from 'vuex'
export default {
  name: 'chat',
  data () {
    return {
      isLoading: false,
      conversations: [
        {
          conversationId: 'ABC123',
          landlordFirstName: 'Jon',
          landlordLastName: 'Preece',
          messages: [{
            conversationId: 'ABC123',
            id: 1,
            message: 'Hello',
            tenantId: 'tenantId',
            landlordId: 'landlordId',
            senderId: 'tenantId',
            sent: '2017-02-08T12:01:24'
          }, {
            conversationId: 'ABC123',
            id: 2,
            message: 'This is a fairly long response, although not epic This is a fairly long response, although not epic This is a fairly long response, although not epic',
            tenantId: 'tenantId',
            landlordId: 'landlordId',
            senderId: 'landlordId',
            sent: '2017-02-08T12:01:24'
          }, {
            conversationId: 'ABC123',
            id: 1,
            message: 'Hello',
            tenantId: 'tenantId',
            landlordId: 'landlordId',
            senderId: 'tenantId',
            sent: '2017-02-08T12:01:24'
          }, {
            conversationId: 'ABC123',
            id: 2,
            message: 'This is a fairly long response, although not epic This is a fairly long response, although not epic This is a fairly long response, although not epic',
            tenantId: 'tenantId',
            landlordId: 'landlordId',
            senderId: 'landlordId',
            sent: '2017-02-08T12:01:24'
          }, {
            conversationId: 'ABC123',
            id: 1,
            message: 'Hello',
            tenantId: 'tenantId',
            landlordId: 'landlordId',
            senderId: 'tenantId',
            sent: '2017-02-08T12:01:24'
          }, {
            conversationId: 'ABC123',
            id: 2,
            message: 'This is a fairly long response, although not epic This is a fairly long response, although not epic This is a fairly long response, although not epic',
            tenantId: 'tenantId',
            landlordId: 'landlordId',
            senderId: 'landlordId',
            sent: '2017-02-08T12:01:24'
          }, {
            conversationId: 'ABC123',
            id: 1,
            message: 'Hello',
            tenantId: 'tenantId',
            landlordId: 'landlordId',
            senderId: 'tenantId',
            sent: '2017-02-08T12:01:24'
          }, {
            conversationId: 'ABC123',
            id: 2,
            message: 'This is a fairly long response, although not epic This is a fairly long response, although not epic This is a fairly long response, although not epic',
            tenantId: 'tenantId',
            landlordId: 'landlordId',
            senderId: 'landlordId',
            sent: '2017-02-08T12:01:24'
          }]
        },
        { conversationId: 'ABC456', landlordFirstName: 'Customer', landlordLastName: 'Support', messages: [] }
      ],
      selectedConversation: {}
    }
  },
  created () {
    this.isLoading = true
    this.$http.get('/api/conversation')
      .then(response => {
        // this.data = response.data
      })
      .finally(() => {
        this.isLoading = false

        if (this.conversations) {
          this.selectedConversation = this.conversations[0]
        }
      })
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
    div {
      justify-content: center;
      align-items: center;
      display: flex;
      height: 100%;
    }
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
</style>

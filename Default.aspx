<%@ Page Title="Home Page" Language="VB" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.vb" Inherits="AspNetSignalRGroupsDemo._Default" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">


    <div class="row">
        <div class="col-md-4">
           <h3>Select a chat room</h3>
           <ul class="messages list-unstyled" data-bind="foreach: chatRooms">
              <li><button type="button" class="btn-link" data-bind="text: Name, click: $root.joinChatRoom, enabled: $root.interactionEnabled"></button></li>
           </ul>  
        </div>
        <div class="col-md-8" data-bind="if: currentChatRoom">
            <h1 data-bind="text: currentChatRoom().Name"></h1>
          <ul id="messages" class="messages list-unstyled" data-bind="foreach: currentChatRoomMessages" style="height:300px; width:360px; border:1px solid #ddd; overflow-y:auto; padding:10px;">
              <li>
                  <div style="font-size:11px; font-style:italic;"><span data-bind="text: Username"></span> at <span data-bind="text: Sent"></span> wrote:</div>
                  <p data-bind="text: Text"></p>
              </li>
          </ul>
          <form data-bind="submit: sendMessage" class="form-inline">
            <input type="text" class="form-control" style="display:inline;" data-bind="value: messageText, enabled: interactionEnabled" />
            <button class="btn btn-primary" data-bind="enabled: interactionEnabled"><i class="glyphicon glyphicon-send"></i> Invia</button>
          </form>
        </div>
    </div>

    <script>
        var chatViewModel = function () {
            var self = this;
            self.chatRooms = ko.observableArray([]);
            self.currentChatRoom = ko.observable(null);
            self.currentChatRoomMessages = ko.observableArray([]);
            self.interactionEnabled = ko.observable(false);
            self.messageText = ko.observable(null);
            
            self.leaveChatRoom = function () {
                if (self.currentChatRoom()) {
                    return $.connection.chatHub.server.leaveChatRoom(self.currentChatRoom().Id);
                } else {
                    return $.when();
                }
            };
            self.joinChatRoom = function (chatRoom) {
                self.leaveChatRoom().done(function () {
                    self.currentChatRoomMessages([]);
                    self.currentChatRoom(chatRoom);
                    $.connection.chatHub.server.joinChatRoom(chatRoom.Id).done(function () {
                        self.scrollToBottom();
                    });
                });
            };
            $.connection.chatHub.client.receiveChatRooms = function (chatRooms) {
                self.chatRooms(chatRooms);
            };
            $.connection.chatHub.client.receiveMessage = function (message) {
                //Il messaggio che ho ricevuto è per la chat corrente?
                if (message.ChatRoomId == self.currentChatRoom().Id) {
                    self.currentChatRoomMessages.push(message);
                    self.scrollToBottom();
                }
            };
            self.sendMessage = function () {
                var message = {
                    chatRoomId: self.currentChatRoom().Id,
                    text: self.messageText()
                };
                self.interactionEnabled(false);
                $.connection.chatHub.server.sendMessage(message).done(function () {
                    self.messageText(null);
                    self.interactionEnabled(true);
                    self.scrollToBottom();
                });
            };

            self.scrollToBottom = function () {
                setTimeout(function () {
                    $("#messages").animate({ scrollTop: $('#messages')[0].scrollHeight }, 500);
                }, 200);
            };

            $.connection.hub.start().done(function () {
                self.interactionEnabled(true);
            })
        }
        ko.applyBindings(new chatViewModel());
    </script>

</asp:Content>

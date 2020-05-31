export class MessageWithReply implements IMessageWithReply {
    message: IHostMessage;
    replyMessage: IReplyMessageData[] | null;

    constructor(
        data: IMessageWithReply
    ) {
        this.message = new HostMessage(data.message);
        if (data.replyMessage) {
            this.replyMessage = data.replyMessage.map(m => {
                return new ReplyMessageData(m);
            });
        } else {
            this.replyMessage = null;
        }
    }
}

export interface IMessageWithReply {
    message: IHostMessage;
    replyMessage: IReplyMessageData[] | null;
}

export class HostMessage implements IHostMessage {
    createdDateTime: Date;
    hostUserId: string;

    constructor(data: IHostMessage) {
        this.createdDateTime = new Date(data.createdDateTime);
        this.hostUserId = data.hostUserId;
    }

}

export interface IHostMessage {
    createdDateTime: Date;
    hostUserId: string;
}

export class ReplyMessageData implements IReplyMessageData {
    createdDateTime: Date;
    content: string;
    userId: string;

    constructor(data: IReplyMessageData) {
        this.createdDateTime = new Date(data.createdDateTime);
        this.content = data.content;
        this.userId = data.userId;
    }
}


export interface IReplyMessageData {
    createdDateTime: Date;
    content: string;
    userId: string;
}

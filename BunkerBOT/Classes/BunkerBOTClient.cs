using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Exceptions;
using Telegram.Bot.Extensions.Polling;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.ReplyMarkups;

namespace BunkerBOT.Classes
{
    class BunkerBOTClient : TelegramBotClient
    {


        public BunkerBOTClient(string token)
            : base(token)
        {
        }

        public void StartReceiving(ReceiverOptions receiverOptions, CancellationTokenSource cancellationTokenSource)
        {
            this.StartReceiving(
                 HandleUpdateAsync,
                 ErrorAsync,
                 receiverOptions,
                 cancellationToken: cancellationTokenSource.Token);

        }



        private async Task HandleUpdateAsync(ITelegramBotClient botClient, Update update, CancellationToken cancellationToken)
        {
            if (update.Type != UpdateType.Message)
                return;

            // Only process text messages
            if (update.Message!.Type != MessageType.Text)
                return;

            //long chaatId = 1394136560;
            long chaatId = 813273041;
            var chatId = update.Message.Chat.Id;
            var userFirstName = update.Message.Chat.FirstName;
            var messageText = update.Message.Text;
            var userName = update.Message.Chat.Username;



            Console.WriteLine($"Получил сообщение '{messageText}' в чате с айди {chatId}. Логин пользователя {userName} ");



            //проверка содержимого собщения через if/else
            if (messageText.Contains("+7") && messageText.Contains("@"))
            {
                //оправляется человеку с конкретным chatid
                Message sendmsses = await botClient.SendTextMessageAsync(
                chatId: chaatId,
                text: $"Босс, я получил новую бронь! '{messageText}' в чате {chatId}, логин {userName}",
                cancellationToken: cancellationToken);

                //отправляется человеку, который работал с ботом
                Message sendmes = await botClient.SendTextMessageAsync(
                    chatId: chatId,
                    text: $"👁🗨*Спасибо, ожидайте! я оповещу вас сразу, как рассмотрят вашу заявку.\n\n Можете использовать меня дальше!*",


                    parseMode: ParseMode.Markdown,
                    cancellationToken: cancellationToken);
            }
            else
            {}

            //проверка содержимого собщения через if/else
            if (messageText.Contains("@") && messageText.Contains("проблема"))
            { 
                //оправляется человеку с конкретным chatid
                Message sendmsses = await botClient.SendTextMessageAsync(
                chatId: chaatId,
                text: $"Босс, у вашего гостя проблема '{messageText}' в чате {chatId}, логин {userName}",
                cancellationToken: cancellationToken);
               
                //отправляется человеку, который работал с ботом
                Message sendmes = await botClient.SendTextMessageAsync(
                chatId: chatId,
                text: $"👁🗨*Спасибо, ожидайте! я или мои руководители оповестят вас!\n\n Можете использовать меня дальше!*",
                parseMode: ParseMode.Markdown,
                cancellationToken: cancellationToken);
            }
            else
            { }

            //проверка содержимого собщения через кейсы
            switch (messageText)
            {
                case "/start":
                    await StartMessaging(botClient, cancellationToken, chatId, userFirstName);
                    break;
                case "👁‍🗨о нас":             
                    await AboutButton(botClient, cancellationToken, chatId);
                    break;
                case "👁‍🗨записаться":
                    //isMarkingLessCommonWordMode = true;
                    //isAddingNewWordMode = false;
                    await RegisterButton(botClient, cancellationToken, chatId, update);
                    break;
                case "👁‍🗨вернуться в меню":
                    //isMarkingLessCommonWordMode = false;
                    //isAddingNewWordMode = false;
                    await BackToMenuButton(botClient, cancellationToken, chatId);
                    break;             
                case "👁‍🗨Текст":
                    //isAddingNewWordMode = true;
                    //isMarkingLessCommonWordMode = false;
                    await textadd(botClient, cancellationToken, chatId);
                    break;
                case "👁‍🗨актульная информация":
                    //isAddingNewWordMode = true;
                    //isMarkingLessCommonWordMode = false;
                    await register(botClient, cancellationToken, chatId);
                    break;
                case "👁‍🗨наши соц. сети":
                    //isAddingNewWordMode = true;
                    //isMarkingLessCommonWordMode = false;
                    await sociable(botClient, cancellationToken, chatId);
                    break;
                case "👁‍🗨назад":
                    //isAddingNewWordMode = true;
                    //isMarkingLessCommonWordMode = false;
                    await nazadvideo(botClient, cancellationToken, chatId);
                    break;
                case "👁‍🗨‍помощь":
                    //isMarkingLessCommonWordMode = true;
                    //isAddingNewWordMode = false;
                    await helpp(botClient, cancellationToken, chatId, update);
                    break;
                case "👁‍🗨‍правила регистрации":
                    //isAddingNewWordMode = true;
                    //isMarkingLessCommonWordMode = false;
                    await RulesSend(botClient, cancellationToken, chatId);
                    break;
            }
        }

 
 


         private async Task StartMessaging(ITelegramBotClient botClient, CancellationToken cancellationToken, long chatId, string userFirstName)
        {


   

          
                ReplyKeyboardMarkup replyKeyboardMarkup = new(new[]
{

                new KeyboardButton[] { "👁‍🗨о нас", "👁‍🗨записаться"},
                new KeyboardButton[] { "👁‍🗨актульная информация", "👁‍🗨наши соц. сети"},
                new KeyboardButton[] { "👁‍🗨‍помощь" },

            })
                {
                    ResizeKeyboard = true
                };




                Message sendGreetingMessage = await botClient.SendTextMessageAsync(
                    chatId: chatId,
                    text: $"*👁‍🗨Здравствуй! Меня зовут Mr. Zimit.*\n\n" +
                          "*Я проводник в бункер города Ульяновска.*\n\n" +
                          "*В нашем бункере, мы проводим интелектуальные*" +
                          $"* испытания, которые тебе помогут развиваться и быть*" +
                          " * умнее.*\n\n " +
                          $"*Кликай по меню, что тебе интересно?*",
                    parseMode: ParseMode.Markdown,
                    replyMarkup: replyKeyboardMarkup,
                    cancellationToken: cancellationToken);
            
        
    

    }
    




            


         private async Task AboutButton(ITelegramBotClient botClient, CancellationToken cancellationToken, long chatId)
            {

                ReplyKeyboardMarkup replyKeyboardMarkup = new(new[]
                {
                new KeyboardButton[] { "👁‍🗨Текст"},
                new KeyboardButton[] { "👁‍🗨вернуться в меню"},
            })
                {
                    ResizeKeyboard = true
                };



                Message sendmess = await botClient.SendTextMessageAsync(
                    chatId: chatId,
                    text: $"*👁‍🗨Окей, я тебя понял*",

                    parseMode: ParseMode.Markdown,
                    replyMarkup: replyKeyboardMarkup,
                    cancellationToken: cancellationToken);


                Message sendmesss = await botClient.SendTextMessageAsync(
        chatId: chatId,
        text:
        $"*Отправить мини-видео о нашем мероприятие или почитать про нас?\n\nКликай по меню👇*",


        parseMode: ParseMode.Markdown,
        replyMarkup: replyKeyboardMarkup,
        cancellationToken: cancellationToken);


            }



         private async Task RulesSend(ITelegramBotClient botClient, CancellationToken cancellationToken, long chatId)
            {
            ReplyKeyboardMarkup replyKeyboardMarkup = new(new[]
            {

                new KeyboardButton[] { "👁‍🗨записаться"},
                new KeyboardButton[] { "👁‍🗨вернуться в меню"},

            })
            {
                ResizeKeyboard = true
            };



                    Message sendmes = await botClient.SendTextMessageAsync(
                    chatId: chatId,
                    text: $"*👁️‍🗨️Регистрация на игру производится только по предоплате.*\n\n *Если по какой-то причине Вы не можете прийти на мероприятие и предупреждаете об этом за 48 часов, предоплата возвращается в полном размере.*\n\n -Если Вы предупреждаете за 24 часа, предоплата возвращается с удержанием 50%.\n\n -Если Вы предупреждаете менее чем за 24 часа, то предоплата не возвращается, однако Ваш визит переносится на другой раз\n\n -Если Вы не пришли на мероприятие, предоплата не возвращается.\n\n *По всем вопросам писать @user* ",
                    parseMode: ParseMode.Markdown,
                    replyMarkup: replyKeyboardMarkup,
                    cancellationToken: cancellationToken);




            }

         private async Task nazadvideo(ITelegramBotClient botClient, CancellationToken cancellationToken, long chatId)
            {

                ReplyKeyboardMarkup replyKeyboardMarkup = new(new[]
             {
                new KeyboardButton[] { "👁‍🗨Текст"},
                new KeyboardButton[] { "👁‍🗨вернуться в меню"},
            })
                {
                    ResizeKeyboard = true
                };



                Message sendmess = await botClient.SendTextMessageAsync(
                chatId: chatId,
                text: $"*👁‍🗨Окей, я тебя понял*",
                parseMode: ParseMode.Markdown,
                replyMarkup: replyKeyboardMarkup,
                cancellationToken: cancellationToken);


                Message sendmesss = await botClient.SendTextMessageAsync(
                chatId: chatId,
                text:
                $"*Отправить мини-видео о нашем мероприятие или почитать про нас?*\n" +
                "*\n\nКликай по меню👇*",
                parseMode: ParseMode.Markdown,
                replyMarkup: replyKeyboardMarkup,
                cancellationToken: cancellationToken);


        }



         private async Task helpp(ITelegramBotClient botClient, CancellationToken cancellationToken, long chatId, Update update)
        {
         
            Message sendmess = await botClient.SendTextMessageAsync(
            chatId: chatId,
            text: $"*👁‍🗨Окей, я тебя понял*",
            parseMode: ParseMode.Markdown,
            cancellationToken: cancellationToken);

            Message sendmdesss = await botClient.SendTextMessageAsync(
            chatId: chatId,
            text: $"*Если у тебя остался какой-то вопрос или образовалась какая-то проблема с работой нашего бота.\r\n\r\nНапиши заявку с описанной проблемой ниже 👇 \r\n\r\n(Не забудь оставить свой логин) Пример:@username , проблема ... *",

    
            parseMode: ParseMode.Markdown,
            replyMarkup: new InlineKeyboardMarkup(
            InlineKeyboardButton.WithUrl(
            text: "на случай, если бот не может решить проблему",
            url: "https://t.me/username")),
            cancellationToken: cancellationToken);
        }





    

         private async Task register(ITelegramBotClient botClient, CancellationToken cancellationToken, long chatId)
            {

                ReplyKeyboardMarkup replyKeyboardMarkup = new(new[]
                {
                new KeyboardButton[] { "👁‍🗨о нас", "👁‍🗨записаться"},
                new KeyboardButton[] { "👁‍🗨вернуться в меню"},

            })
                {
                    ResizeKeyboard = true
                };

                Message sendmes = await botClient.SendTextMessageAsync(
                    chatId: chatId,
                    text: $"*👁‍🗨Окей, я тебя понял*",

                    parseMode: ParseMode.Markdown,
                    replyMarkup: replyKeyboardMarkup,
                    cancellationToken: cancellationToken);


            Message message = await botClient.SendPhotoAsync(
                chatId: chatId,
                photo: "https://imgur.com/MEI7Qxo",
                caption: $"🖤УЛЬЯНОВСК. БУНКЕР. ЦЕХ. и ничего лишнего.🖤 \n\n 🗨Мы ответственно подошли к прокачке нашего мероприятия и теперь: \n\n Теперь наш БУНКЕР мероприятие с особыми призами за *выигрыш, полностью альтернативный нетворкинг.* Помимо всего этого, ты приходишь в уютную компанию, где можешь *заказывать себе алкоголь и покушать по себестоимости.* \n\n И так, меньше слов - больше дела! \n\n *🗨ЦЕХ 15.12\nул. Ленина, 116а* *18:30 сбор гостей \r\n19:00 начало*\n\n *Орг сбор 1000 руб. (не пугайся, подробнее о наших скидках ты можешь уточнить у нашего бота)*\n\n Ждем тебя! Твое присутствие обязательно!",

                parseMode: ParseMode.Markdown,
                cancellationToken: cancellationToken);

    
        }

         private async Task sociable(ITelegramBotClient botClient, CancellationToken cancellationToken, long chatId)
            {

             

                Message sendmes = await botClient.SendTextMessageAsync(
                chatId: chatId,
                text: $"*👁‍🗨Окей, я тебя понял*",
                parseMode: ParseMode.Markdown,
                cancellationToken: cancellationToken);

                Message sendmses = await botClient.SendTextMessageAsync(
                chatId: chatId,
                text: $"*👁‍🗨Наши соц. сети:\r\n\r\nVK: https://vk.com/bunker.club73\r\nInsta: в разработке*",
                parseMode: ParseMode.Markdown,
                cancellationToken: cancellationToken);


        }

         private async Task textadd(ITelegramBotClient botClient, CancellationToken cancellationToken, long chatId)
            {

                ReplyKeyboardMarkup replyKeyboardMarkup = new(new[]
                {
                new KeyboardButton[] { "👁‍🗨записаться","👁‍🗨вернуться в меню"},
                new KeyboardButton[] { "👁‍🗨назад"},

            })
                {
                    ResizeKeyboard = true
                };


                Message sendmes = await botClient.SendTextMessageAsync(
                    chatId: chatId,
                    text: $"*👁‍🗨Окей, я тебя понял*",

                    parseMode: ParseMode.Markdown,
                    replyMarkup: replyKeyboardMarkup,
                    cancellationToken: cancellationToken);

            Message message = await botClient.SendPhotoAsync(
                chatId: chatId,
                photo: "https://imgur.com/XNgpVA9",
                parseMode: ParseMode.Markdown,
                cancellationToken: cancellationToken);

            Message sendMessage = await botClient.SendTextMessageAsync(
            chatId: chatId,
            text: "👁‍🗨 Мы организуем мероприятие, на которое люди приходят отдохнуть, поблтать и поиграть в диалоговую игру бункер.\n\n" +
            "🗨*Почему тебе стоит прийти и попробовать?* \nКак минимум ты заведешь новых друзей и по настоящему отдохнешь. С нами ты можешь пробывать себя и развиваться.\nТакже мы просто кайфуем, курим кальяны и общаемся.\n\n" +
            "🗨*Что за игра такая?*\nНаступил апокалипсис и бункер рассчитан на определенное количество мест (а вас много) и твоя первочередная задача доказать свою правоту и выжить.\n\n" +
            "У тебя есть 6 карт, в которых описана вся твоя новая личность:" +
            "*-проффесия* (программист, сантехник, врач,...)\n-*хобби* (краеведение, свингервечеринки,...)\n-*здоровье* (импотент, нет ноги, ...)\n-*биология* (гей, транс и т.д.)\n-*твои факты* (псих, стоишь над душой, ...)\n-*и то что у тебя с собой* (пистолет, бензопила, чемоданчик фельдшера,...)\n" +
            "Твоя задача имея эти данные доказать -  для чего и зачем тебя должны взять в бункер.\n\n" +
            "🗨* Что еще?*\nВ игре и до нее мы прокачиваем свой интелект путем общения, выполняя некоторые упражнения для повышения таких навыков как:\n" +
            "-самопрезентация\n-исскуство продаж\n-коммуникабельность \n-исскуство переговоров.\n\n" +
            "В нашем обществе эти навыки необходимы. Даже если ты уже как-то в этом преуспел, мы тебе обязательно усложним задачу, чтобы тебе самому было интереснее.\n" +
            "\n👁‍🗨*Хочешь записаться? Кликай по меню.*",
            parseMode: ParseMode.Markdown,
            replyMarkup: replyKeyboardMarkup,
            cancellationToken: cancellationToken);


            }


         private async Task RegisterButton(ITelegramBotClient botClient, CancellationToken cancellationToken, long chatId, Update update)
            {
                ReplyKeyboardMarkup replyKeyboardMarkup = new(new[]
                {
                new KeyboardButton[] { "👁‍🗨‍правила регистрации" },
                new KeyboardButton[] { "👁‍🗨вернуться в меню"},

            })
                {
                    ResizeKeyboard = true
                };


                Message sendmes = await botClient.SendTextMessageAsync(
                    chatId: chatId,
                    text: $"*👁‍🗨Окей, я тебя понял*",

                    parseMode: ParseMode.Markdown,
                    replyMarkup: replyKeyboardMarkup,
                    cancellationToken: cancellationToken);


            Message message = await botClient.SendPhotoAsync(
                chatId: chatId,
                photo: "https://i.imgur.com/i8tRnnN.jpg",
                parseMode: ParseMode.Markdown,
                cancellationToken: cancellationToken);

            Message sendmses = await botClient.SendTextMessageAsync(
             chatId: chatId,
             text: $"👁🗨*Оу! Ты хочешь попасть в бункер? … Хорошо* \n\n *❗️Орг. сбор  1000 руб., но также у тебя есть возможность сэкономить 200 рублей (подробнее о правилах регистрации смотри в меню). \n\nПереводить деньги по номеру: \nСбербанк: +7 (906) 390 24 72 (Иван Андреевич Г.)\n\n\n- Сделать репост нашей публикации (анонса) в Instagram.\r\n- Приведи одного или двух друзей. Как ты получишь скидку, так и они.\n\n По вопросам системы скидок писать @ivanglebovv \n\n ❗️Мы работаем по системе бронирования места, поэтому для бронирования потребуется предоплата. (подробнее о правилах регистрации смотри в меню).\n\n*",
             parseMode: ParseMode.Markdown,
            replyMarkup: new InlineKeyboardMarkup(
            InlineKeyboardButton.WithUrl(
            text: "Место проведения",
            url: "https://yandex.ru/maps/-/CCUnZAvskA")),
            cancellationToken: cancellationToken);



            Message sendes = await botClient.SendTextMessageAsync(
           chatId: chatId,
           text: $"*👁🗨Что мне от тебя требуется? Оставь заявку и с тобой свяжется мой босс:\n\n- Никнейм Instagram | твой номер телефона (в одном сообщении)\n\nПример: @username +79999999999 \nПрисылай👇 И жди ответа!*",
           parseMode: ParseMode.Markdown,
           replyMarkup: replyKeyboardMarkup,
           cancellationToken: cancellationToken);





            }











        private async Task BackToMenuButton(ITelegramBotClient botClient, CancellationToken cancellationToken, long chatId)
            {
                ReplyKeyboardMarkup replyKeyboardMarkup = new(new[]
                {
                new KeyboardButton[] { "👁‍🗨о нас", "👁‍🗨записаться"},
                new KeyboardButton[] { "👁‍🗨актульная информация", "👁‍🗨наши соц. сети"},
                new KeyboardButton[] { "👁‍🗨‍помощь" },

            })


                {
                    ResizeKeyboard = true
                };


                Message sendmes = await botClient.SendTextMessageAsync(
                    chatId: chatId,
                    text: $"*👁‍🗨Окей, я тебя понял*",

                    parseMode: ParseMode.Markdown,
                    replyMarkup: replyKeyboardMarkup,
                    cancellationToken: cancellationToken);

                Message sendGreetingMessage = await botClient.SendTextMessageAsync(
                   chatId: chatId,
                    text:
                          $"👁‍🗨Кликай по меню, что тебе интересно?",
                   replyMarkup: replyKeyboardMarkup,
                   cancellationToken: cancellationToken);
            }



        private Task ErrorAsync(ITelegramBotClient botClient, Exception exception, CancellationToken cancellationToken)
        {
            var ErrorMessage = exception switch
            {
                ApiRequestException apiRequestException
                    => $"Telegram API Error:\n[{apiRequestException.ErrorCode}]\n{apiRequestException.Message}",
                _ => exception.ToString()
            };

            Console.WriteLine($"Exception caught: {ErrorMessage}");
            return Task.CompletedTask;
        }

    }
}

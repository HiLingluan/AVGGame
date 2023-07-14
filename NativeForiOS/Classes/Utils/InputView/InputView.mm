//
//  InputView.m
//  Unity-iPhone
//
//  Created by 玩贝 on 2018/12/20.
//

#import "InputView.h"
#import "Macro.h"

@interface InputView() <UITextViewDelegate>

@property(nonatomic,strong)UITextView *textView;

@property(nonatomic,strong)UILabel *hinttextView;

@property(nonatomic,strong)UIButton *inputBackgroundView;

@property(nonatomic,strong)UIView *toolView;

@property(nonatomic,assign)CGFloat keyboardHeight;

@property (strong, nonatomic) UILabel *stirngLenghLabel;

@property(nonatomic,strong)UILabel *hintLabel;

@property (strong, nonatomic)UIButton *sendBtn;

@property (strong, nonatomic)UIButton *allScreenButton;


@end

@implementation InputView
{
    int _edkeyboardHeight;
    int _ed_minlimit;
    int _ed_limit;
    NSString *_edinputContent;
    NSString *_ed_content;
    NSString *_ed_tag;
    NSString *_ed_hint;
}

/*
// Only override drawRect: if you perform custom drawing.
// An empty implementation adversely affects performance during animation.
- (void)drawRect:(CGRect)rect {
    // Drawing code
}
*/

- (instancetype)initWithFrame:(CGRect)frame Edit:(int )mId limit:(int)limit str:(NSString *)str hint:(NSString *)hint tag:(NSString *)tag
{
    self = [super initWithFrame:frame];
    if (self) {
        [self showEdit:mId limit:limit str:str hint:hint tag:tag];
    }
    
    
    
    return self;
}

- (void)dealloc
{
    [[NSNotificationCenter defaultCenter] removeObserver:self];
}

- (void)showEdit:(int )mId limit:(int)limit str:(NSString *)str hint:(NSString *)hint tag:(NSString *)tag
{

    _ed_minlimit  = mId;
    _ed_limit = limit;
    _ed_content= str;
    _ed_hint = hint;
    _ed_tag= tag;
    _edinputContent = @"";  //需要初始化
    
    CGFloat width=SCREEN_WIDTH;
    CGFloat height=SCREEN_WIDTH+100;
    
    self.allScreenButton =[UIButton new];
    self.allScreenButton.frame=CGRectMake(0, 0, SCREEN_WIDTH, SCREEN_HEIGHT);
    self.allScreenButton.backgroundColor=[UIColor colorWithRed:245/255.0 green:1/255.0 blue:245/255.0 alpha:0];
    [self.allScreenButton addTarget:self action:@selector(allScreenBtnHandler) forControlEvents:UIControlEventTouchUpInside];
    [self addSubview:self.allScreenButton];
    
    
    self.inputBackgroundView.frame=CGRectMake(0, height, width, 100);
    [self addSubview:self.inputBackgroundView];
    [self.inputBackgroundView addSubview:self.toolView];
    
    
    self.textView.frame=CGRectMake(15, 5, width-95, 85);
    [self.inputBackgroundView addSubview:self.textView];
    
    
    //键盘的frame即将发生变化时立刻发出该通知
    [[NSNotificationCenter defaultCenter]addObserver:self selector:@selector(keyboardChanged:) name:UIKeyboardWillChangeFrameNotification object:nil];
    [[NSNotificationCenter defaultCenter]addObserver:self selector:@selector(keyboardHide:) name:UIKeyboardWillHideNotification object:nil];
    
    [self showEdit];
}

-(UIView *)toolView{
    
    if (_toolView==nil) {
        _toolView=[UIView new];
        _toolView.backgroundColor=[UIColor colorWithRed:244/255.0 green:244/255.0 blue:244/255.0 alpha:1];
        _toolView.frame=CGRectMake(0, 0, SCREEN_WIDTH, 100);
        
        self.sendBtn=[UIButton buttonWithType:UIButtonTypeCustom];
        [self.sendBtn setTitle:@"" forState:UIControlStateNormal];
        [self.sendBtn setTitleColor:[UIColor whiteColor] forState:UIControlStateNormal];
        self.sendBtn.titleLabel.font=[UIFont systemFontOfSize:13];
        [self.sendBtn setBackgroundImage:[UIImage imageNamed:@"sendbtnbg"] forState:UIControlStateNormal];
        [self.sendBtn setBackgroundImage:[UIImage imageNamed:@"sendbtnbg_grey"] forState:UIControlStateDisabled];
        [self.sendBtn addTarget:self action:@selector(sendContent) forControlEvents:UIControlEventTouchUpInside];
        self.sendBtn.frame=CGRectMake(SCREEN_WIDTH-60, 5 ,50, 50);
        [_toolView addSubview:self.sendBtn];
        
        //实时显示字数
        self.stirngLenghLabel =[UILabel new];
        self.stirngLenghLabel.frame=CGRectMake(SCREEN_WIDTH-78, 50 ,80, 50);
        self.stirngLenghLabel.textAlignment = NSTextAlignmentCenter;
        self.stirngLenghLabel.lineBreakMode = NSLineBreakByCharWrapping;
        self.stirngLenghLabel.font = [UIFont systemFontOfSize:15];
        self.stirngLenghLabel.textColor = [UIColor grayColor];
        self.stirngLenghLabel.text =  @"";
        [_toolView addSubview:self.stirngLenghLabel];
        
        
        UIView *topLineView=[UIView new];
        topLineView.backgroundColor=[UIColor colorWithRed:238/255.0 green:238/255.0 blue:238/255.0 alpha:1];
        topLineView.frame=CGRectMake(0, 0, SCREEN_WIDTH, 1);
        [_toolView addSubview:topLineView];
    }
    return _toolView;
}

-(UIButton *)inputBackgroundView{
    
    if (_inputBackgroundView==nil) {
        _inputBackgroundView=[UIButton new];
        _inputBackgroundView.backgroundColor=[UIColor colorWithRed:245/255.0 green:245/255.0 blue:245/255.0 alpha:1];
    }
    return _inputBackgroundView;
}

-(UITextView *)textView{
    
    if (_textView==nil) {
        _textView=[[UITextView alloc]init];
        _textView.font=[UIFont systemFontOfSize:20];
        _textView.text = _ed_content;
        _textView.layer.cornerRadius=10;
        _textView.layer.masksToBounds=YES;
        _textView.tintColor = [UIColor colorWithRed:240/255.0 green:98/255.0 blue:65/255.0 alpha:1];
        //_textView.layer.borderWidth=3;
        //_textView.layer.borderColor=[UIColor colorWithRed:245/255.0 green:245/255.0 blue:245/255.0 alpha:1].CGColor;
        //_textView.layer.backgroundColor =[UIColor colorWithRed:245/255.0 green:245/255.0 blue:245/255.0 alpha:1].CGColor;
        _textView.backgroundColor = [UIColor whiteColor];
        _textView.delegate=self;
        
        self.hintLabel= [UILabel new];
        self.hintLabel.frame=CGRectMake(5,-10,290,60);
        self.hintLabel.text = _ed_hint;
        self.hintLabel.textAlignment = NSTextAlignmentLeft;
        self.hintLabel.textColor =[UIColor grayColor];
        self.hintLabel.font = [UIFont systemFontOfSize:20];
        [_textView addSubview:self.hintLabel];
        
        if (self.hintLabel != nil) {
            if(_ed_content.length== 0){
                self.hintLabel.hidden = NO;
            }else{
                self.hintLabel.hidden = YES;
            }
        }
        
    }
    return _textView;
}

-(void)keyboardChanged:(NSNotification *)notification{
    
    NSLog(@"%@",notification);
    
    CGRect frame=[notification.userInfo[UIKeyboardFrameEndUserInfoKey]CGRectValue];
    
    CGRect currentFrame=self.inputBackgroundView.frame;
    
    [UIView animateWithDuration:0.25 animations:^{
        
        //输入框最终的位置
        CGRect resultFrame;
        
        if (frame.origin.y==SCREEN_HEIGHT) {
            resultFrame=CGRectMake(currentFrame.origin.x, SCREEN_HEIGHT-currentFrame.size.height, currentFrame.size.width, currentFrame.size.height);
            self.keyboardHeight=0;
//            UnitySendMessage("UIOther", "OnKeyBoardHide", [[NSString stringWithFormat:@"%f",self.keyboardHeight] UTF8String]);
        }else{
            resultFrame=CGRectMake(currentFrame.origin.x,SCREEN_HEIGHT-currentFrame.size.height-frame.size.height , currentFrame.size.width, currentFrame.size.height);
            self.keyboardHeight=frame.size.height;
//            UnitySendMessage("UIOther", "OnKeyBoardShow", [[NSString stringWithFormat:@"%f",self.keyboardHeight] UTF8String]);
        }
        self->_edkeyboardHeight  = self.keyboardHeight;
        self.inputBackgroundView.frame=resultFrame;
        
    }];
    
}

- (void)keyboardHide:(NSNotification *)notification
{
    
    [UIView animateWithDuration:0.25 animations:^{
        CGRect currentFrame=self.inputBackgroundView.frame;
        CGRect resultFrame;
        resultFrame=CGRectMake(currentFrame.origin.x, SCREEN_HEIGHT-currentFrame.size.height, currentFrame.size.width, currentFrame.size.height);
        self.keyboardHeight=0;
//        UnitySendMessage("UIOther", "OnKeyBoardHide", [[NSString stringWithFormat:@"%f",self.keyboardHeight] UTF8String]);
        self->_edkeyboardHeight  = self.keyboardHeight;
        self.inputBackgroundView.frame=resultFrame;
    }];
}

-(void)textViewDidChange:(UITextView *)textView{
    NSString *str=textView.text;
    if (self.stirngLenghLabel !=nil){
        //字数限制操作
        if (str.length >= _ed_limit) {
            textView.text = [textView.text substringToIndex:_ed_limit];
            _edinputContent = [[NSString alloc] initWithString:textView.text ];
            self.stirngLenghLabel.text = [NSString stringWithFormat:@"%d/%d", _ed_limit,_ed_limit];
        }else{
            self.stirngLenghLabel.text =  [NSString stringWithFormat:@"%lu/%d", (unsigned long)textView.text.length,_ed_limit];
            _edinputContent = [[NSString alloc] initWithString:textView.text ];
        }
//        UnitySendMessage("CommonManager", "InputContentChange", [_edinputContent UTF8String]);
    }
    
    if (self.hintLabel != nil) {
        if(str.length== 0){
            self.hintLabel.hidden = NO;
        }else{
            self.hintLabel.hidden = YES;
        }
    }
    
    //取消按钮点击权限，并显示提示文字
    if (textView.text.length <_ed_minlimit || textView.text.length>_ed_limit) {
        self.sendBtn.enabled = NO;
    }else{
        self.sendBtn.enabled = YES;
    }
}


//点击空白区域
-(void)allScreenBtnHandler{
    
    [self.textView resignFirstResponder];
    self.hidden = YES;
}

-(void)showEdit
{
    [UIView animateWithDuration:0.25 animations:^{
        [self.textView becomeFirstResponder];
        [self textViewDidChange:self.textView];
        self.hidden = NO;
    }];
}


-(void)sendContent{
    //[self.view endEditing:YES];
    if (_textView!= nil){
        NSString *str=_textView.text;
        if (str.length > _ed_limit  || str.length <_ed_minlimit) {
            NSLog(@"out of limit!!!!!!");
        }else{
//            UnitySendMessage("UIOther", "OnEditInfoBack", [str UTF8String]);
        }
    }
}

//获取键盘高度
-(int)getKeyBoardHeight;
{
    return _edkeyboardHeight;
}



//获取输入框区域高度
-(NSString*)getInputAreaHeight
{
    return [NSString stringWithFormat:@"%d",(_edkeyboardHeight +100)];
}


//获取输入框内容
-(NSString*)getInputAreaConent
{
    return _edinputContent;
}

@end
